FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
COPY ./bin/Release/netcoreapp6.0/publish .
EXPOSE 5000
EXPOSE 5001
ENTRYPOINT ["dotnet", "Api.dll"]



#FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
#WORKDIR /app
#EXPOSE 5000
#EXPOSE 443

#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
#WORKDIR /src
#COPY ["Api/Api.csproj", "Api/"]
#RUN dotnet restore "Api/Api.csproj"
#COPY . .
#WORKDIR "/src/Api"
#RUN dotnet build "Api.csproj" -c Release -o /app/build

#FROM build AS publish
#RUN dotnet publish "Api.csproj" -c Release -o /app/publish

#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "Api.dll"]



#  dotnet dev-certs https -ep ~/.aspnet/https/api.pfx -p "13Friends)"
#  dotnet dev-certs https --trustcd 
#  dotnet user-secrets -p Api/Api.csproj set "Kestrel:Certificates:Development:Password" "13Friends)"

# for https
# openssl pkcs12 -export -out aspnetapp.pfx -inkey privkey.pem -in fullchain.pem -certfile fullchain.pem 

#PARENT="localhost"
#openssl req \
#-x509 \
#-newkey rsa:4096 \
#-sha256 \
#-days 365 \
#-nodes \
#-keyout $PARENT.key \
#-out $PARENT.crt \
#-subj "/CN=${PARENT}" \
#-extensions v3_ca \
#-extensions v3_req \
#-config <( \
#  echo '[req]'; \
#  echo 'default_bits= 4096'; \
#  echo 'distinguished_name=req'; \
#  echo 'x509_extension = v3_ca'; \
#  echo 'req_extensions = v3_req'; \
#  echo '[v3_req]'; \
#  echo 'basicConstraints = CA:FALSE'; \
#  echo 'keyUsage = nonRepudiation, digitalSignature, keyEncipherment'; \
#  echo 'subjectAltName = @alt_names'; \
#  echo '[ alt_names ]'; \
#  echo "DNS.1 = www.${PARENT}"; \
#  echo "DNS.2 = ${PARENT}"; \
#  echo '[ v3_ca ]'; \
#  echo 'subjectKeyIdentifier=hash'; \
#  echo 'authorityKeyIdentifier=keyid:always,issuer'; \
#  echo 'basicConstraints = critical, CA:TRUE, pathlen:0'; \
#  echo 'keyUsage = critical, cRLSign, keyCertSign'; \
#  echo 'extendedKeyUsage = serverAuth, clientAuth')
#  
#  openssl x509 -noout -text -in $PARENT.crt
#  openssl pkcs12 -export -out api.pfx -inkey $PARENT.key -in $PARENT.crt