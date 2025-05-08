$ldapServer = "198.51.100.11"
$ldapPort = 389
$timeout = 5

try {
    $tcpConnection = Test-NetConnection -ComputerName $ldapServer -Port $ldapPort -InformationLevel Detailed
    if ($tcpConnection.TcpTestSucceeded) {
        Write-Host "Conexión LDAP exitosa"
    } else {
        Write-Host "No se pudo conectar al servidor LDAP"
    }
} catch {
    Write-Host "Error al intentar conectar al servidor LDAP: $_"
}