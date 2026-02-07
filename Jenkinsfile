
pipeline {
    agent any

    environment {
        SOLUTION = 'Agreement.Web.sln'
        BUILD_DIR = 'publish'
        IIS_SITE = 'AgreementWeb'
        PUBLISH_PATH = 'C:\\inetpub\\AgreementWeb'
        MSBUILD = '"C:\\Program Files (x86)\\Microsoft Visual Studio\\2019\\BuildTools\\MSBuild\\Current\\Bin\\MSBuild.exe"'
    }

    stages {

        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Restore NuGet') {
            steps {
                bat '"C:\\nuget\\nuget.exe" restore %SOLUTION%'
            }
        }

        stage('Build') {
            steps {
                bat '''
                %MSBUILD% %SOLUTION% /p:Configuration=Release
                '''
            }
        }

        stage('Publish') {
            steps {
                bat '''
                %MSBUILD% %SOLUTION% /p:DeployOnBuild=true ^
                /p:PublishProfile=FolderProfile ^
                /p:PublishUrl=%BUILD_DIR%
                '''
            }
        }

        stage('Deploy to IIS') {
            steps {
                bat '''
                iisreset /stop
                xcopy %BUILD_DIR% %PUBLISH_PATH% /E /Y /I
                iisreset /start
                '''
            }
        }
    }

    post {
        success {
            echo 'Deployment to IIS successful 🎉'
        }
        failure {
            echo 'Deployment failed ❌'
        }
    }
}

