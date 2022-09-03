import { User } from "./user"
import { Trade } from "./trade";

export interface Journal {
    id: number;
    name: string;
    description: string;
    dateCreated: Date;
    appUser: User | null;
    appUserId: number;
    trades?: Trade[] | null;
}

export interface JournalDto {
    name: string;
    description: string;
}