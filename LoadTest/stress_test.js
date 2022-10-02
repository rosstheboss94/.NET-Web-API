import http from 'k6/http';
import { sleep } from 'k6';

export let options = {
	insecureSkipTLSVerify: true,
	noConnectionReuse: false,
	stages: [
		{ duration: '2m', target: 250 },
		{ duration: '5m', target: 250 },
		{ duration: '2m', target: 500 },
		{ duration: '5m', target: 500 },
		{ duration: '2m', target: 750 },
		{ duration: '5m', target: 750 },
		{ duration: '2m', target: 1000 },
		{ duration: '5m', target: 1000 },
		{ duration: '10m', target: 0 },
	],
};

const API_BASE_URL = 'http://localhost:5001/api/v1';

export default () => {
	const reqheaders = {
		Authorization:
			'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InVzZXIiLCJlbWFpbCI6InVzZXJAdXNlci5jb20iLCJuYmYiOjE2NjQ0OTgwODYsImV4cCI6MTY2NDUwMTY4NiwiaWF0IjoxNjY0NDk4MDg2fQ.6iSzYrrxy_mJqVBvM3x7cRpXuZnVvQOVrUlkuxoGxAI',
	};

	http.batch([
		{
			method: 'GET',
			url: `${API_BASE_URL}/Journal/user/journals`,
			params: { headers: reqheaders },
		},
		{
			method: 'GET',
			url: `${API_BASE_URL}/Trade/user/journal/1/trades`,
			params: { headers: reqheaders },
		},
	]);

	sleep(10);
};
