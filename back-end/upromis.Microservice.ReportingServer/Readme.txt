Before running the ReportingServer microserver, make sure a RabbitMQ server is running
on localhost, using the standard ports of RabbitMQ.

A docker container for this exists, and can be started like this:

docker run -d --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management

(-d runs it in the background)
(--rm ensures that it is removed once it is stopped)

to stop this container from running, use

docker stop rabbitmq

