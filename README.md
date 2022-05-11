# Weather-Forecast-Microservice
## Rodando a Aplicação
### Requisitos
- .NET 5
- Docker
### Executando
Os comandos abaixo iram subir um serviço de mensageria RabbitMQ e um Banco de Dados MongoDB:
```
docker run -it --rm -d --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.9-management
```
```
docker run -it --rm -d --name forecastdb -p 27017:27017 mongo:4.4.6
```
Após o clone do repositório, execute na raiz do projeto execute o seguinte comando:
```
dotnet run --project .\Weather.Microservice1\
```
Em um outro terminal execute o comando abaixo:
```
dotnet run --project .\Weather.Microservice2\
```
### Exemplo de Uso
Os testes podem ser feitos via swagger pelo link abaixo:
https://localhost:5001/swagger/index.html

Uma chamada post no endpoint abaixo solicita o clima de Cascavel:
```
https://localhost:5001/api/ForecastData?cityName=Cascavel
```
Uma chamada get no endpoint abaixo lista as informações armazenadas no banco:
```
https://localhost:5001/api/ForecastData
```
## Objetivos
Desenvolver uma solução Microservices que permite cadastrar e consultar previsão do tempo.  

![image](https://user-images.githubusercontent.com/6729346/167315861-89364c2b-41ed-4521-9715-b71906552b2f.png)

## Especificações
**Microservice1** 
 - Responsável por expor API Rest para cadastrar previsão do tempo 
 - Deve produzir uma mensagem para o broker requisitando a operação de Consulta de Previsão  de Tempo. 
 - Deve escutar a resposta do broker com a previsão de tempo e armazenar em um Banco de  Dados 

**Microservice2** 
 - Responsável por integrar com API [Open Weather](http://openweathermap.org/api)
 - Deve se registrar no broker para escutar mensagens de Consulta de Previsão de Tempo 
 - Efetuar um HTTP para api do Open Weather, a reposta da API deve ser publicada no broker. 

![WeatherForecastMicroservice drawio](https://user-images.githubusercontent.com/6729346/167316019-87280017-cfc7-482d-8684-47d9d8ce57e5.png)

## TO-DO
- [X] Criar Microsserviço que se comunicará com o Open Weather
- [X] Fazer a comunicação entre APIs via RestSharp
- [X] Criar o Microsserviço que vai persisir os dados no Banco
- [X] Comunicar com o banco MongoDB
- [ ] ~Orquestrar Container com o Microsserviço e Banco~
- [X] Fazer comunicação entre os Microsserviços via RabbitMQ
- [ ] ~Adicionar Microsserviço 2 e o RabbitMQ ao Container~
- [ ] Refatorar

## Skills Necessárias
- Arquitetura de Microsserviços
- Orquestração de Containers com Docker
- Comunicação entre Microsservições
- Boas Práticas na Criação de Microsserviços
