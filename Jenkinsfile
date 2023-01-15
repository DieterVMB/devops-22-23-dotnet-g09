pipeline {
         agent any      

    stages {
        stage("Generating testing report") {
             agent{
        docker{
            image 'mcr.microsoft.com/playwright/dotnet:v1.28.0-focal'
        }
    }
    environment {
        HOME = '/tmp'
    } 

              
            steps {
                sh'''
                dotnet build VirtualIT/Tests 
                dotnet dev-certs https
                cd VirtualIT/Server
                nohup dotnet run --urls=http://localhost:5000 &>/dev/null &
                sleep 20
                cd ..
                dotnet new tool-manifest --force
                dotnet tool install coverlet.console
                dotnet tool install --local dotnet-reportgenerator-globaltool
                dotnet tool restore
                dotnet coverlet Tests/bin/Debug/net6.0/PlaywrightTests.dll --target "dotnet" --targetargs "test --no-build" --format opencover -o="coverage.xml"
                dotnet reportgenerator -reports:"coverage.xml" -targetdir:"coveragereport" -reporttypes:Html
                '''
            }   
        }

        stage("Serving testing report"){

            steps {
                sh '''
                cp -R /var/lib/jenkins/workspace/first-pipeline@2/VirtualIT/coveragereport/* /var/lib/jenkins/coveragereport
                '''
            }      
        }



        stage("static code analysis"){
             environment {
                SONAR_TOKEN_NET = credentials('sonar')
             }  


            steps {
                    sh'''
                    dotnet /var/lib/jenkins/tools/hudson.plugins.sonar.MsBuildSQRunnerInstallation/SonarScanner_for_MSBuild/SonarScanner.MSBuild.dll begin /k:dotnetg9 /d:sonar.cs.opencover.reportsPaths=/var/lib/jenkins/workspace/first-pipeline@2/VirtualIT/coverage.xml /d:sonar.login=$SONAR_TOKEN_NET
                    dotnet build /var/lib/jenkins/workspace/first-pipeline/VirtualIT
                    dotnet /var/lib/jenkins/tools/hudson.plugins.sonar.MsBuildSQRunnerInstallation/SonarScanner_for_MSBuild/SonarScanner.MSBuild.dll end /d:sonar.login=$SONAR_TOKEN_NET
                    '''
                      
            }      
        }

        stage("Build the image"){

            steps {
                sh '''
                cd VirtualIT
                docker build -t victorwillem/netappvirtualithogent:latest .
                '''

            }
        }

        stage("Login"){

        environment {
        DOCKERHUB_CREDENTIALS = credentials('dockerhub')
        }     

            steps {
                sh '''
                echo $DOCKERHUB_CREDENTIALS_PSW | docker login -u $DOCKERHUB_CREDENTIALS_USR --password-stdin
                '''

            }
        }

        stage("Push de image naar dockerhub"){
   

            steps {
                sh '''
                docker push victorwillem/netappvirtualithogent:latest
                '''

            }
        }
        }

        post {
            always{
                 sh '''
                docker logout
                docker system prune -f
                '''
            }
        }
       
    }   
