# Etapa de Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

RUN apt-get update && apt-get install -y libc6-dev

# Define o diretório de trabalho para o build
WORKDIR /app

# Copia o arquivo de projeto para o contêiner
COPY ToDo.csproj /src/ToDo/

# Define o diretório de trabalho para o projeto
WORKDIR /src/ToDo

# Restaura as dependências
RUN dotnet restore "ToDo.csproj"

# Copia o restante do código para o contêiner
COPY ./ /src/ToDo/

# Publica o projeto
RUN dotnet publish -c Release -o /app/out

# Etapa de Execução
FROM mcr.microsoft.com/dotnet/sdk:8.0

# Define o diretório de trabalho para a execução da aplicação
WORKDIR /app

# Copia a aplicação publicada da etapa anterior
COPY --from=build-env /app/out ./ 

COPY dockerconfig.env ./dockerconfig.env

# Expõe a porta para acessar a aplicação
EXPOSE 8080

# Define o comando para iniciar a aplicação
ENTRYPOINT ["dotnet", "ToDo.dll"]
