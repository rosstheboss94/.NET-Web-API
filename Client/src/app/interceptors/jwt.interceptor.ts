import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable, take } from 'rxjs';
import { UserService } from '../services/user.service';
import { User } from '../models/user';
import { environment } from 'src/environments/environment';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  baseUrl = environment.apiUrl;

  constructor(private userService: UserService) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    let currentUser: User;

    this.userService.currentUser$
      .pipe(take(1))
      .subscribe((user) => (currentUser = user));

    if (
      request.url == `${this.baseUrl}/user/login` ||
      request.url == `${this.baseUrl}/user/register`
    )
      return next.handle(request);

    request = request.clone({
      setHeaders: {
        Authorization: `Bearer ${currentUser.token}`,
      },
    });

    return next.handle(request);
  }
}
