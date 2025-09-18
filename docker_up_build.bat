@echo off
echo Starting Docker services...
docker-compose up -d --build
echo Docker services started.
pause