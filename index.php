<?php
//连接数据库
$conn = pg_connect("host=127.0.0.1 port=5432 dbname=yiibai_db user=postgres password=123");
$str = "select * from \"Test\"";
$ret = pg_query($conn,$str);
while($row = pg_fetch_row($ret)){
    echo $row[0];//输出数据库ID[1]
    echo $row[1];//输出数据库ID[2]
}

//连接SOCKET
$host = "192.168.19.97";
$port= "3355";
$socket = socket_create(AF_INET, SOCK_STREAM, SOL_TCP);
$result=socket_connect($socket,$host,$port);
    $budder=@socket_read($socket,1024);//接受Scket数据
    echo $budder;
    $userid=$_POST['msg'];//获取unity发来数据
    socket_write($socket,$userid); //将unity数据发送到socket
    $budder=@socket_read($socket,1024);//重新接受unity数据
    echo $budder;
socket_close($socket);
?>  