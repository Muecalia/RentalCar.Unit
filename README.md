# RentalCar.Unit
Microserviço para gestão das unidades da loja de aluguer de carros. Unidades estas onde os cliente podem deixar e ou levantar os automoveis.

# Arquitectura do Projecto
![Diagrama](https://github.com/user-attachments/assets/3f4e648a-f0dd-4e5f-b633-d5fdbe90098a)

# Entities
### Model
| Type         | Variavel    | Descrition |
|--------------|-------------|------------|
| string       | Id          |            |
| string       | Name        |            |
| string       | Address     |            |
| string       | Phone       |            |
| DateTime     | CreatedAt   |            |
| DateTime     | UpdatedAt   |            |
| DateTime     | DeletedAt   |            |
| bool         | IsDeleted   |            |
<br/>

# Auxiliary Projects
| Project      | Link       | 
|--------------|----------------------------------------------------|
| Security     | https://github.com/Muecalia/RentalCar.Security     |

<br/>

# Linguagens, Ferramentas e Tecnologias
<div align="left">
  <p align="left">
    <a href="https://go-skill-icons.vercel.app/">
      <img src="https://go-skill-icons.vercel.app/api/icons?i=cs,dotnet,mysql,rabbitmq,git,kubernetes,docker,sonarqube,swagger,postman,githubactions,aws" />
    </a>
  </p>
</div> <br/>

# Monitoramento
<div align="left">
  <p align="left">
    <a href="https://go-skill-icons.vercel.app/">
      <img src="https://go-skill-icons.vercel.app/api/icons?i=prometheus,grafana" />
    </a>
  </p>
</div> <br/>

# Observabilidade e Tracing
![Jaeger_OpenTelemetry](https://github.com/user-attachments/assets/bac7e17b-c42c-48a8-83ab-c0c3c1b0f3dc)

<br/>

# Migration
dotnet ef migrations add FirstMigration --project RentalCar.Unit.Infrastructure -o Persistence/Migrations -s RentalCar.Unit.API
dotnet ef database update --project RentalCar.Unit.Infrastructure -s RentalCar.Unit.API
