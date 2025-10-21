# Alias build env para a 
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
# Definindo o diretório de trabalho
WORKDIR /app

# Copia codigo fonte para o container. Params (src - origem dos dados, . - local onde os arquivos serão colados (destino))
COPY src/ .

# Entra na pasta de api
WORKDIR /app/CashFlow.Api

# Executa o comando na pasta atual - dotnet restore - identifica e restaura as dependencias e ferramentas de um projeto
RUN dotnet restore

# Publica a api com a configuração release e salva na pasta app/out (criada em tempo de execução)
RUN dotnet publish -c Release -o /app/out
# 'publish' - Publica o projeto (compila e prepara os arquivos para implantação).
# '-c Release' - Define a config de compilação. Release, otimizada para produção.
# '-o /app/out' - Especifica o diretório de saída, onde os arquivos publicados serão colocados.

# Criando outra imagem para executar a API
FROM mcr.microsoft.com/dotnet/aspnet:9.0 
# Criando novo diretório para a nova imagem
WORKDIR /app

# Copiando todos os arquivos da pasta out para o diretório APP da minha nova imagem
COPY --from=build-env /app/out .
# '--from=build' - Copiando da imagem anterior.

# Executa esse comando 'dotnet CashFlow.Api.dll'
ENTRYPOINT [ "dotnet", "CashFlow.Api.dll" ]