# Etapa de Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

RUN apt-get update && apt-get install -y libc6-dev

# Define o diret�rio de trabalho para o build
WORKDIR /app

# Copia o arquivo de projeto para o cont�iner
COPY ToDo.csproj /src/ToDo/

# Define o diret�rio de trabalho para o projeto
WORKDIR /src/ToDo

# Restaura as depend�ncias
RUN dotnet restore "ToDo.csproj"

# Copia o restante do c�digo para o cont�iner
COPY ./ /src/ToDo/

# Publica o projeto
RUN dotnet publish -c Release -o /app/out

# Etapa de Execu��o
FROM mcr.microsoft.com/dotnet/sdk:8.0

# Define o diret�rio de trabalho para a execu��o da aplica��o
WORKDIR /app

# Copia a aplica��o publicada da etapa anterior
COPY --from=build-env /app/out ./ 

COPY dockerconfig.env ./dockerconfig.env

# Exp�e a porta para acessar a aplica��o
EXPOSE 8080

# Define o comando para iniciar a aplica��o
ENTRYPOINT ["dotnet", "ToDo.dll"]
