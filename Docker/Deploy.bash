IP="217.29.62.245"
USER="root"

docker login --username valeribenua --password "98ade691-0a37-41d1-afca-9e6113a8008b"

# Сделать билд

# Собрать image

## Запушить image
#echo 1. ----------------------------- Запушить image start
#docker push valeribenua/api:1
##sleep 10
#echo    ---------- Запушить image finish
#echo 

# Остановить запущенный на сервере container
echo 2. ----------------------------- Остановить запущенный на сервере container start
ssh ${USER}@${IP} docker stop root_api_1
ssh ${USER}@${IP} docker rm root_api_1
echo    ---------- Остановить запущенный на сервере container finish
echo

# Получить новый image
echo 3. ----------------------------- Получить новый image start
ssh ${USER}@${IP} docker pull valeribenua/api:1
echo    ---------- Получить новый image finish
echo

# Запустить container 
echo 4. ----------------------------- Запустить container start
ssh ${USER}@${IP} docker-compose up -d
echo    ---------- Запустить container finish
