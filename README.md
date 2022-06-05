# khra-scan
.NET cli multi threaded host discovery tool.

scans a subnet range for a specific port and report hosts having that port open
 
khra-scan.exe int threads, string Subnet,string start_sub, string end_sub ,string start_address,string end_address,int port,int timeout

exemple :
khra-scan.exe 200 192.168. 135 235 1 254 445 3

```
=> scans :
    with 200 threads
    from 192.168.135.1 to 168.235.254 
    port 445
    timeout 3 seconds
```
compile with NET 3.5 for windows 7
        and NET 4.5 for windows 10
# todo list:
- clean the ugliness (made in few minutes in a hurry sorry xD)
- scan from CIDR
- add port scan
- host discovery with UDP broadcast ?
- make as dll for dynamic assembly loading
