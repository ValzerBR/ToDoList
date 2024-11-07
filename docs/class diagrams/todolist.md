# Diagrama de Classes do ToDo List

Este diagrama mostra as classes principais do sistema de ToDo List.

```mermaid
classDiagram
    class Tarefa {
        +int Id
        +string Titulo
        +string Descricao
        +string Status
        +Date DataDeCriacao
        +Date? DataDeEncerramento
        +Date DataDeVencimento
        +int UsuarioId
    }

    class Usuario {
        +int Id
        +string Nome
        +string Email
    }

    class Categoria {
        +int Id
        +string Nome
    }

    Tarefa "1" --> "*" Categoria : eh_categorizada_por
    Usuario "1" --> "*" Categoria : cria
    