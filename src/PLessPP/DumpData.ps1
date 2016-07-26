$Listener = [System.Net.Sockets.TcpListener]7039
$Listener.Start()
echo "Start app now"
$Connection = $Listener.AcceptTcpClient()
$Reader = [System.IO.StreamReader]$Connection.GetStream()
while ($true) {
  $Reader.ReadLine();
}