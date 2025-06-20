kubectl apply -f .\platforms-depl.yaml
kubectl apply -f .\platforms-nodeport-service.yaml
kubectl apply -f .\commands-depl.yaml 
 
kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.12.3/deploy/static/provider/cloud/deploy.yaml
 
Write-Output "Waiting for Ingress-nginx deployment"
kubectl wait --namespace ingress-nginx --for=condition=ready pod --selector=app.kubernetes.io/component=controller --timeout=120s
Write-Output "Ingress-nginx deployment finished"
 
kubectl apply -f .\ingress-service.yaml 
kubectl apply -f .\local-pvc.yaml 

# This password is supposed to be set manually from the commandline, so that we don't have this stored in a file 
$mssql_password = "pa55w0rd!"
kubectl create secret generic mssql --from-literal=SA_PASSWORD=$mssql_password