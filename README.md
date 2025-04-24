# OutboxCoffee

OutboxCoffee é um projeto de demonstração que implementa o Padrão Outbox (Outbox Pattern) em uma aplicação .NET para garantir consistência entre operações de banco de dados e o envio de mensagens em um sistema distribuído.

## Sobre o Projeto

Este projeto simula um sistema de pedidos de café onde:

1. Um cliente faz um pedido de café
2. O pedido é salvo no banco de dados
3. Uma notificação é enviada para o sistema de preparo através de um message broker (RabbitMQ)

O Padrão Outbox é implementado para garantir que, mesmo em caso de falhas, o sistema mantenha a consistência entre o banco de dados e as mensagens enviadas.

## Estrutura do Projeto

O projeto está organizado em várias camadas:

- **OutboxCoffee.API**: API REST para receber pedidos de café
- **OutboxCoffee.Application**: Camada de aplicação com serviços e interfaces
- **OutboxCoffee.Core**: Entidades de domínio e interfaces de repositório
- **OutboxCoffee.Infrastructure**: Implementações de repositórios e infraestrutura
- **OutboxCoffee.Worker**: Processador de mensagens da tabela outbox

## Implementação do Padrão Outbox

O Padrão Outbox é implementado da seguinte forma:

1. Quando um pedido é criado, além de salvar o pedido no banco de dados, uma mensagem é inserida na tabela `OutboxMessages`
2. Um worker processa periodicamente as mensagens não processadas da tabela outbox
3. O worker publica essas mensagens no RabbitMQ e as marca como processadas

Este padrão garante que as mensagens só serão enviadas se a transação do banco de dados for confirmada com sucesso, evitando inconsistências entre o banco de dados e o message broker.

## Como Executar

### Pré-requisitos

- .NET 8.0 ou superior
- Docker e Docker Compose

### Passos

1. Clone o repositório:
   ```
   git clone https://github.com/rfulgencio3/OutboxCoffee.git
   ```

2. Navegue até a pasta do projeto:
   ```
   cd OutboxCoffee
   ```

3. Inicie os serviços com Docker Compose:
   ```
   docker-compose up -d
   ```

4. Execute a API:
   ```
   cd src/OutboxCoffee.API
   dotnet run
   ```

5. Em outro terminal, execute o worker:
   ```
   cd src/OutboxCoffee.Worker
   dotnet run
   ```
* Ou se prefefir, pode-se utilizar o a opção no Visual Studio de "Configure Startup Projects > Multiple startup projects" que permite a execução de mais de um projeto simultâneamente.
Assim, basta selecionar os projetos OutboxCoffee.API e OutboxCoffee.Worker. 

## Melhorias Recomendadas

### Melhorias Futuras

- Estratégia de arquivamento para a tabela outbox
- Mecanismo de retry com backoff exponencial
- Monitoramento e logging
- Testes automatizados
- Configuração flexível
- Documentação de API com Swagger
- Health checks

## Referências

- [Microservices.io - Transactional Outbox Pattern](https://microservices.io/patterns/data/transactional-outbox.html)
- [Medium - Outbox Pattern for Microservices Architectures](https://medium.com/design-microservices-architecture-with-patterns/outbox-pattern-for-microservices-architectures-1b8648dfaa27)

## Licença

Este projeto está licenciado sob a licença MIT.
