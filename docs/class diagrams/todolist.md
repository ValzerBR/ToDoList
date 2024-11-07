# Diagrama de Classes do ToDo List

Este diagrama mostra as classes principais do sistema de ToDo List.

```mermaid
classDiagram
    class Tarefa {
        +int Id
        +string Titulo
        +string Descricao
        +Status Status
        +Date DataDeCriacao
        +Date? DataDeEncerramento
        +Date DataDeVencimento
        +int UsuarioId
    }

    class Status {
        <<enumeration>>
        +Pendente
        +EmAndamento
        +Concluida
        +Cancelada
        +Vencida
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
    