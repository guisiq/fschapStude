# Use a imagem base .NET
FROM mcr.microsoft.com/devcontainers/dotnet:1-8.0-bookworm


#Instale o PowerShell

RUN apt-get update && apt-get install -y curl gnupg apt-transport-https && \
    curl -fsSL https://packages.microsoft.com/keys/microsoft.asc | apt-key add - && \
    echo "deb [arch=amd64] https://packages.microsoft.com/repos/microsoft-debian-stretch-prod stretch main" > /etc/apt/sources.list.d/powershell.list && \
    apt-get update && \
    apt-get install -y powershell


# Instale o Microsoft Build do OpenJDK 11
RUN apt-get install -y msopenjdk-11

# Instale o Graphviz
RUN apt-get install -y graphviz

# Instale o nvm e o Node.js
RUN curl -o- https://raw.githubusercontent.com/nvm-sh/nvm/v0.38.0/install.sh | bash && \
    . ~/.nvm/nvm.sh && \
    nvm install 14.17.0

# Instale o OpenJDK e o Graphviz
# RUN apt-get update && \ 
#     apt-get install -y openjdk-11-jdk && \ 
#     apt-get install -y graphviz

# # Instale o nvm e o Node.js
# RUN curl -o- https://raw.githubusercontent.com/nvm-sh/nvm/v0.38.0/install.sh | bash && \
#     . ~/.nvm/nvm.sh && \
#     nvm install 14.17.0