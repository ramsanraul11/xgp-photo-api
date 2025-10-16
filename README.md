# Run docker
docker run --name postgres-xgp -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=tuPassword -e POSTGRES_DB=XgpPhotoDb -p 5432:5432 -d postgres:16
