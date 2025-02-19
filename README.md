# YouTube-DotNetMicroservices
[Course] .NET Microservices follow-along code for YouTube course.

## How to run
To run the PlatformService from the docker file execute the following commands:

To build the docker image, from inside YoutubeCourse.PlatformService:<br>
``docker build -t alvinbrz/platformservice .``

After that, to run the image (swagger will not be available by default, we should add a env variable to select either development or release mode during publish):<br>
``docker run -p 8080:8080 -d alvinbrz/platformservice``

Finally, to upload the image to Docker Hub:<br>
``docker push alvinbrz/platformservice``
