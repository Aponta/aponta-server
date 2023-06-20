cd C:\Users\semantix1\OneDrive - SEMANTIX TECNOLOGIA EM SISTEMA DE INFORMACAO S.A\Documentos\Projetos Pessoais\Aponta\aponta-server\aponta-server
docker image rm image-aponta-server --force
docker build --no-cache -f Dockerfile -t image-aponta-server .
heroku container:push web -a aponta-server
heroku container:release web -a aponta-server
pause .