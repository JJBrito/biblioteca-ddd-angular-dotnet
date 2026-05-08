import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';

import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatTableModule } from '@angular/material/table';

import { Livro } from '../../models/livro.model';
import { LivroService } from '../../services/livro.service';

@Component({
  selector: 'app-livro-list',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatTableModule,
    MatButtonModule,
    MatInputModule,
    MatCardModule,
    MatIconModule,
    MatSnackBarModule,
    MatProgressSpinnerModule
  ],
  templateUrl: './livro-list.component.html',
  styleUrl: './livro-list.component.scss'
})
export class LivroListComponent implements OnInit {
  livros: Livro[] = [];

  carregando = false;

  displayedColumns = [
    'titulo',
    'autor',
    'genero',
    'anoPublicacao',
    'disponivel',
    'acoes'
  ];

  filtroForm!: ReturnType<FormBuilder['group']>;

  livroForm!: ReturnType<FormBuilder['group']>;

  constructor(
    private readonly livroService: LivroService,
    private readonly formBuilder: FormBuilder,
    private readonly snackBar: MatSnackBar
  ) {
    this.filtroForm = this.criarFiltroForm();
    this.livroForm = this.criarLivroForm();
  }

  ngOnInit(): void {
    this.carregarLivros();
  }

  carregarLivros(): void {
    this.carregando = true;

    this.livroService.obterTodos().subscribe({
      next: livros => {
        this.livros = livros;
        this.carregando = false;
      },
      error: erro => {
        console.error('Erro ao carregar livros:', erro);
        this.carregando = false;
        this.exibirMensagem('Não foi possível carregar os livros.');
      }
    });
  }

  filtrar(): void {
    const { titulo, autor, genero } = this.filtroForm.value;

    this.carregando = true;

    this.livroService
      .filtrar(titulo ?? '', autor ?? '', genero ?? '')
      .subscribe({
        next: livros => {
          this.livros = livros;
          this.carregando = false;

          if (livros.length === 0) {
            this.exibirMensagem('Nenhum livro encontrado para o filtro informado.');
            return;
          }

          this.exibirMensagem('Filtro aplicado com sucesso.');
        },
        error: erro => {
          console.error('Erro ao filtrar livros:', erro);
          this.carregando = false;
          this.exibirMensagem('Não foi possível filtrar os livros.');
        }
      });
  }

  limparFiltro(): void {
    this.filtroForm.reset();
    this.carregarLivros();
    this.exibirMensagem('Filtro limpo.');
  }

  salvar(): void {
    if (this.livroForm.invalid) {
      this.livroForm.markAllAsTouched();
      this.exibirMensagem('Preencha todos os campos obrigatórios.');
      return;
    }

    const request = {
      titulo: this.livroForm.value.titulo ?? '',
      autor: this.livroForm.value.autor ?? '',
      genero: this.livroForm.value.genero ?? '',
      anoPublicacao: Number(this.livroForm.value.anoPublicacao)
    };

    this.carregando = true;

    this.livroService.criar(request).subscribe({
      next: () => {
        this.resetarLivroForm();
        this.carregarLivros();
        this.exibirMensagem('Livro cadastrado com sucesso.');
      },
      error: erro => {
        console.error('Erro ao salvar livro:', erro);
        this.carregando = false;
        this.exibirMensagem('Não foi possível cadastrar o livro.');
      }
    });
  }

  remover(id: string): void {
    const confirmar = confirm('Deseja remover este livro?');

    if (!confirmar) {
      return;
    }

    this.carregando = true;

    this.livroService.remover(id).subscribe({
      next: () => {
        this.carregarLivros();
        this.exibirMensagem('Livro removido com sucesso.');
      },
      error: erro => {
        console.error('Erro ao remover livro:', erro);
        this.carregando = false;
        this.exibirMensagem('Não foi possível remover o livro.');
      }
    });
  }

  campoInvalido(nomeCampo: string): boolean {
    const campo = this.livroForm.get(nomeCampo);

    return !!campo && campo.invalid && (campo.dirty || campo.touched);
  }

  private criarFiltroForm(): ReturnType<FormBuilder['group']> {
    return this.formBuilder.group({
      titulo: [''],
      autor: [''],
      genero: ['']
    });
  }

  private criarLivroForm(): ReturnType<FormBuilder['group']> {
    return this.formBuilder.group({
      titulo: ['', Validators.required],
      autor: ['', Validators.required],
      genero: ['', Validators.required],
      anoPublicacao: [
        new Date().getFullYear(),
        [
          Validators.required,
          Validators.min(1),
          Validators.max(new Date().getFullYear())
        ]
      ]
    });
  }

  private resetarLivroForm(): void {
    this.livroForm.reset({
      titulo: '',
      autor: '',
      genero: '',
      anoPublicacao: new Date().getFullYear()
    });
  }

  private exibirMensagem(mensagem: string): void {
    this.snackBar.open(mensagem, 'Fechar', {
      duration: 3000,
      horizontalPosition: 'right',
      verticalPosition: 'top'
    });
  }
}