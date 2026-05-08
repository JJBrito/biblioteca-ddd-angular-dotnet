export interface Livro {
  id: string;
  titulo: string;
  autor: string;
  genero: string;
  anoPublicacao: number;
  disponivel: boolean;
  criadoEm: string;
}

export interface CriarLivroRequest {
  titulo: string;
  autor: string;
  genero: string;
  anoPublicacao: number;
}

export interface AtualizarLivroRequest {
  titulo: string;
  autor: string;
  genero: string;
  anoPublicacao: number;
}