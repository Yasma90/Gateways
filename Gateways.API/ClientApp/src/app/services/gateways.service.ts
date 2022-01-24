import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { MessageService } from './message.service';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { API_Url } from 'urlconfig';
import { Gateway } from '../models/gateway.model';

@Injectable({
  providedIn: 'root'
})
export class GatewaysService {

  constructor(
    private http: HttpClient,
    private messageService: MessageService) { }

  formData: Gateway = new Gateway();
  readonly baseUrl = `${API_Url}/Gateways`;
  list: Gateway[];

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Access-Control-Allow-Origin': '*',
      'Access-Control-Allow-Headers': 'Content-Type',
      'Access-Control-Allow-Methods': 'GET,POST,OPTIONS,DELETE,PUT' })
  };

  postGateway(){
    return this.http.post(this.baseUrl,this.formData);
  }

  putGateway(){
    return this.http.put(`${this.baseUrl}/${this.formData.id}`,this.formData);
  }

  deleteGateway(id: number){
    return this.http.delete(`${this.baseUrl}/${id}`);
  }

  refreshList(){
    this.http.get(this.baseUrl)
    .toPromise()
    .then(res=>this.list = res as Gateway[]);
  }

  /** GET gateways from the server */
  getGateways(): Observable<Gateway[]> {
    return this.http.get<Gateway[]>(this.baseUrl)
      .pipe(
        tap(_ => this.log('fetched gateways')),
        catchError(this.handleError<Gateway[]>('getGateways', []))
      );
  }

   /** GET gateway by id. Return `undefined` when id not found */
   getGatewayNo404<Data>(id: number): Observable<Gateway> {
    const url = `${this.baseUrl}/?id=${id}`;
    return this.http.get<Gateway[]>(url)
      .pipe(
        map(gateways => gateways[0]), // returns a {0|1} element array
        tap(h => {
          const outcome = h ? `fetched` : `did not find`;
          this.log(`${outcome} gateway id=${id}`);
        }),
        catchError(this.handleError<Gateway>(`getGateway id=${id}`))
      );
  }

  /** GET gateway by id. Will 404 if id not found */
  getGateway(id: number): Observable<Gateway> {
    const url = `${this.baseUrl}/${id}`;
    return this.http.get<Gateway>(url).pipe(
      tap(_ => this.log(`fetched gateway id=${id}`)),
      catchError(this.handleError<Gateway>(`getGateway id=${id}`))
    );
  }

  /* GET gateways whose name contains search term */
  searchGateway(term: string): Observable<Gateway[]> {
    if (!term.trim()) {
      // if not search term, return empty gateway array.
      return of([]);
    }
    return this.http.get<Gateway[]>(`${this.baseUrl}/?name=${term}`).pipe(
      tap(x => x.length ?
         this.log(`found Gateway matching "${term}"`) :
         this.log(`no gateways matching "${term}"`)),
      catchError(this.handleError<Gateway[]>('searchGateways', []))
    );
  }

  //////// Save methods //////////

  /** POST: add a new gateway to the server */
  addGateway(gateway: Gateway): Observable<Gateway> {
    return this.http.post<Gateway>(this.baseUrl, gateway, this.httpOptions).pipe(
      tap((newGateway: Gateway) => this.log(`added gateway w/ id=${newGateway.id}`)),
      catchError(this.handleError<Gateway>('addGateway'))
    );
  }

  /** DELETE: delete the gateway from the server */
  delGateway(id: number): Observable<Gateway> {
    const url = `${this.baseUrl}/${id}`;

    return this.http.delete<Gateway>(url, this.httpOptions).pipe(
      tap(_ => this.log(`deleted gateway id=${id}`)),
      catchError(this.handleError<Gateway>('deleteGateway'))
    );
  }

  /** PUT: update the gateway on the server */
  upGateway(gateway: Gateway): Observable<any> {
    return this.http.put(this.baseUrl, gateway, this.httpOptions).pipe(
      tap(_ => this.log(`updated gateway id=${gateway.id}`)),
      catchError(this.handleError<any>('updateGateway'))
    );
  }

   /**
   * Handle Http operation that failed.
   * Let the app continue.
   * @param operation - name of the operation that failed
   * @param result - optional value to return as the observable result
   */
    private handleError<T>(operation = 'operation', result?: T) {
      return (error: any): Observable<T> => {

        // TODO: send the error to remote logging infrastructure
        console.error(error); // log to console instead

        // TODO: better job of transforming error for user consumption
        this.log(`${operation} failed: ${error.message}`);

        // Let the app keep running by returning an empty result.
        return of(result as T);
      };
    }

    /** Log a GatewayService message with the MessageService */
    private log(message: string) {
      this.messageService.add(`GatewayService: ${message}`);
    }

}
