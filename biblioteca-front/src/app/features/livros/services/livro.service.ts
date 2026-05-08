import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import {
  Livro,
  CriarLivroRequest,
  AtualizarLivroRequest
} from '../models/livro.model';

@Injectable({
  providedIn: 'root'
})
export class LivroService {

  private readonly apiUrl = 'https://localhost:7269/api/Livros';

  constructor(
    private readonly http: HttpClient
  ) {}

  obterTodos(): Observable<Livro[]> {
    return this.http.get<Livro[]>(this.apiUrl);
  }

  obterPorId(id: string): Observable<Livro> {
    return this.http.get<Livro>(`${this.apiUrl}/${id}`);
  }

  criar(request: CriarLivroRequest): Observable<Livro> {
    return this.http.post<Livro>(this.apiUrl, request);
  }

  atualizar(
    id: string,
    request: AtualizarLivroRequest
  ): Observable<void> {

    return this.http.put<void>(
      `${this.apiUrl}/${id}`,
      request
    );
  }

  remover(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  filtrar(
    titulo?: string,
    autor?: string,
    genero?: string
  ): Observable<Livro[]> {

    let params = new HttpParams();

    if (titulo) {
      params = params.set('titulo', titulo);
    }

    if (autor) {
      params = params.set('autor', autor);
    }

    if (genero) {
      params = params.set('genero', genero);
    }

    return this.http.get<Livro[]>(
      `${this.apiUrl}/filtrar`,
      { params }
    );
  }
}