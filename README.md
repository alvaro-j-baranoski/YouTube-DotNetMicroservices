# YouTube-DotNetMicroservices
[Course] .NET Microservices follow-along code for [Les Jackson YouTube course](https://www.youtube.com/watch?v=DgVjEo3OGBI).

I stopped at Starting with SQL Server chapter.

## How to run
To run the PlatformService from the docker file execute the following commands:

To build the docker image, from inside YoutubeCourse.PlatformService:<br>
``docker build -t alvinbrz/platformservice .``

After that, to run the image (swagger will not be available by default, we should add a env variable to select either development or release mode during publish):<br>
``docker run -p 8080:8080 -d alvinbrz/platformservice``

Finally, to upload the image to Docker Hub:<br>
``docker push alvinbrz/platformservice``

To run the project using kubernetes, go to the K8S folder, and run the ``deploy.ps1``
script, which will fire up all deployments and services.

To stop the project, in the K8S folder, run the ``delete.ps1`` script.

You can check the status using:<br>
``kubectl get services|pods|deployments``

To check which port is being made available in the localhost, check the output
of the NodePort service when running ``kubectl get services``.

## Observation about ports
In the tutorial, the instructor uses .NET 5, which uses port 80 for the ASP.NET
service. In .NET 8, this port is now 8080, so keep that in mind when configuring
the ports in Docker/K8S.

## API Gateway
This project uses ingress-nginx as the API gateway. To run it go to their 
[documentation website](https://kubernetes.github.io/ingress-nginx/deploy/) and grab the ``kubectl apply`` script.

To query deployments made by this script, we need to run ``kubectl get ... --namespace=ingress-nginx``.

After that, its required to add the hostname, which was registered in the ingress-service.yaml file, in the hosts folder
of your computer. In Windows this file is located at C:\Windows\System32\drivers\etc\hosts. Add this line to the file:

``127.0.0.1 acme.com``

After setting this up, we should be able to make a successful GET request to http://acme.com/api/platforms/.