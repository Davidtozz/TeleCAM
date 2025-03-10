#!/bin/bash
command=$1  # first argument: start, stop, etc.

case "$command" in
  start)
    echo "Starting services..."
    docker desktop start
    docker start telecam-db-dev | /dev/null
    echo "Starting dev database..."
    
    # Start API in a background process
    dotnet watch --project ./Api/Api.csproj 
    ;;
    
  stop)
    echo "Stopping services..."
    # Kill dotnet process
    pkill -f "dotnet.*Api.csproj"
    docker stop telecam-db-dev
    docker desktop stop
    ;;
    
  *)
    echo "Usage: $0 {start|stop}"
    echo "  start: Start services"
    echo "  stop: Stop services"
    exit 1
    ;;
esac