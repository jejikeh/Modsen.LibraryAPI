# Modsen.LibraryAPI

CRUD Web API для имитации библиотеки (создание, изменение, удаление, получение)

# Стек
* EF Core
* Postgres
* Docker
* Tye
* MediatR
* AutoMapper
* RabbitMQ

# Функционал

### ```/api/users/register/```

# Запуск проекта
Для запуска проектов в docker conatiner`ах можно использовать [tye](https://github.com/dotnet/tye). Для этого нужно запустить следующую команду:
```bash
tye run # для вывода логов используется --logs console
```
База данных запускается отдельно через docker-compose:
```bash
docker-compose up -d
```