import { Injectable } from '@angular/core'; 
@Injectable() 
export class Constants {
public static readonly API_ENDPOINT: string = 'http://localhost:5000'; 
public readonly API_MOCK_ENDPOINT: string = 'https://wlocalhost:5000/'; 
public static TitleOfSite: string = " Making API For ASK"; 
} 