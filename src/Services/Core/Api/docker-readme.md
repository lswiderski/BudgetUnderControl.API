**Docker build instruction**


docker build -t buc-api .


docker login neufrin.jfrog.io

docker tag buc-api neufrin.jfrog.io/default-docker-virtual/buc-api:latest

docker push neufrin.jfrog.io/default-docker-virtual/buc-api:latest


**Docker compose**

docker compose up