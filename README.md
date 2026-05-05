# 💈 Sistema de Agendamento — Barbearia

Sistema completo de agendamento online para barbearias, com painel administrativo para o barbeiro e tela de agendamento para os clientes.

## 🌐 Deploy

🔗 **Site do cliente:** https://lovely-heart-production.up.railway.app

🔧 **Painel do barbeiro:** https://lovely-heart-production.up.railway.app/painel.html

📄 **Documentação da API:** https://lovely-heart-production.up.railway.app/swagger

---

## 🚀 Tecnologias

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-316192?style=for-the-badge&logo=postgresql&logoColor=white)
![JWT](https://img.shields.io/badge/JWT-000000?style=for-the-badge&logo=jsonwebtokens&logoColor=white)
![HTML5](https://img.shields.io/badge/HTML5-E34F26?style=for-the-badge&logo=html5&logoColor=white)
![CSS3](https://img.shields.io/badge/CSS3-1572B6?style=for-the-badge&logo=css3&logoColor=white)
![JavaScript](https://img.shields.io/badge/JavaScript-F7DF1E?style=for-the-badge&logo=javascript&logoColor=black)
![Railway](https://img.shields.io/badge/Railway-0B0D0E?style=for-the-badge&logo=railway&logoColor=white)

---

## 📋 Funcionalidades

### 👤 Área do Cliente
- ✅ Calendário mensal com dias disponíveis
- ✅ Seleção de horários livres em tempo real
- ✅ Agendamento com nome e telefone
- ✅ Confirmação visual do agendamento
- ✅ Sem necessidade de cadastro ou login

### ✂️ Painel do Barbeiro
- ✅ Login seguro com autenticação JWT
- ✅ Calendário de gestão — ativar/desativar dias de trabalho
- ✅ Visualização da agenda por dia
- ✅ Cancelamento de agendamentos
- ✅ Estatísticas de atendimentos do dia e do mês

### 🔒 API REST
- ✅ Endpoints protegidos por JWT
- ✅ Endpoints públicos para agendamento
- ✅ Validação de horários ocupados
- ✅ Documentação automática com Swagger
- ✅ Deploy em produção com banco de dados real

---

## ⏰ Horários de Atendimento

```
Manhã:  08:00  08:30  09:00  09:30  10:00  10:30  11:00  11:30
Tarde:  14:00  14:30  15:00  15:30  16:00  16:30  17:00  17:30
```
Cada atendimento tem duração fixa de **30 minutos** — totalizando **16 horários por dia**.

---

## 🗄️ Banco de Dados

```
Tabelas:
├── Barbeiros      → autenticação do painel
├── DiasTrabalho   → dias disponíveis para agendamento
└── Agendamentos   → nome, telefone, data, horário e status
```

---

## 🗂️ Arquitetura do Projeto

```
BarbeariaApi/
├── Controllers/
│   ├── AuthController.cs         → login do barbeiro
│   ├── AgendamentoController.cs  → CRUD de agendamentos
│   ├── BarbeiroController.cs     → gestão de dias de trabalho
│   └── HorarioController.cs      → horários disponíveis
├── Services/
│   ├── AgendamentoService.cs     → regras de negócio
│   └── HorarioService.cs         → lógica dos slots de horário
├── Repositories/
│   ├── AgendamentoRepository.cs  → acesso ao banco
│   └── DiaTrabalhoRepository.cs  → acesso ao banco
├── Models/
│   ├── Agendamento.cs
│   ├── DiaTrabalho.cs
│   └── Barbeiro.cs
├── DTOs/
│   └── AgendamentoDTO.cs
├── Data/
│   └── AppDbContext.cs
└── wwwroot/
    ├── index.html     ← tela do cliente
    └── painel.html    ← painel do barbeiro
```

---

## 🔌 Endpoints da API

| Método | Rota | Acesso | Descrição |
|--------|------|--------|-----------|
| `POST` | `/api/auth/setup` | Público | Cria o barbeiro (primeira vez) |
| `POST` | `/api/auth/login` | Público | Login do barbeiro |
| `GET` | `/api/barbeiro/dias-disponiveis/{ano}/{mes}` | Público | Lista dias disponíveis |
| `POST` | `/api/barbeiro/dias-trabalho` | 🔒 JWT | Ativa/desativa dia de trabalho |
| `GET` | `/api/horario/{data}` | Público | Horários disponíveis no dia |
| `POST` | `/api/agendamento` | Público | Cria agendamento |
| `GET` | `/api/agendamento/data/{data}` | 🔒 JWT | Lista agendamentos do dia |
| `GET` | `/api/agendamento/mes/{ano}/{mes}` | 🔒 JWT | Lista agendamentos do mês |
| `PATCH` | `/api/agendamento/{id}/cancelar` | 🔒 JWT | Cancela agendamento |

---

## ⚙️ Como rodar localmente

### Pré-requisitos
- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/download/)

### Passo a passo

**1 — Clone o repositório**
```bash
git clone https://github.com/HonnyedsonCruz/barbearia-agendamento.git
cd barbearia-agendamento
```

**2 — Configure o banco de dados**

Crie o banco no PostgreSQL:
```sql
CREATE DATABASE barbearia_db;
```

No arquivo `appsettings.json`, ajuste a connection string:
```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=barbearia_db;Username=postgres;Password=SUA_SENHA"
}
```

**3 — Rode as migrations**
```bash
dotnet ef database update
```

**4 — Inicie a aplicação**
```bash
dotnet run
```

**5 — Acesse no navegador**
```
http://localhost:5001          → tela do cliente
http://localhost:5001/painel.html  → painel do barbeiro
http://localhost:5001/swagger  → documentação da API
```

**6 — Crie o barbeiro**

No Swagger, acesse `POST /api/auth/setup`:
```json
{
  "nome": "Nome do Barbeiro",
  "email": "barbeiro@email.com",
  "senha": "suasenha"
}
```

---

## 📱 Screenshots

### Tela do Cliente
> Calendário com dias disponíveis → seleção de horário → confirmação

### Painel do Barbeiro
> Gestão de dias de trabalho → visualização da agenda → cancelamento de horários

---

## 👨‍💻 Autor

Desenvolvido por **Honnyedson Cruz**

[![LinkedIn](https://img.shields.io/badge/LinkedIn-0077B5?style=for-the-badge&logo=linkedin&logoColor=white)](https://linkedin.com/in/honnyedson-cruz)
[![GitHub](https://img.shields.io/badge/GitHub-181717?style=for-the-badge&logo=github&logoColor=white)](https://github.com/HonnyedsonCruz)
