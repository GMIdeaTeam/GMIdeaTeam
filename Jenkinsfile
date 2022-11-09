node {
        def app
        stage('Clone repository') {
                git 'https://github.com/zzadu/GMIdeaTeam.git'
        }
        stage('Build image') {
                app = docker.build("zzadu/docker")
        }
        stage('Build Game') {
                app.inside {
                        sh '${UnityPath}/Unity -batchmode -quit -logFile "${PWD}/Build.log" -buildTarget StandaloneWindows -projectPath '/GMIdeaTeam' -executeMethod Build.BuildStandaloneWindows'
		}
	}
        stage('Push image') {
                docker.withRegistry('https://registry.hub.docker.com', 'dockerhub') {
                app.push("${env.BUILD_NUMBER}")
                app.push("latest")
                }
        }
}
