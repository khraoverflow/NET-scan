# NET-scan
.NET cli multi threaded host discovery tool (and port scanner ... at some point ..eventually).

scans a subnet range for a specific port and report hosts having that port open
 
 simple exemple commands:
 ```
 net-scan.exe hosts -r 192.168.1.1-254 -p 445
 ```
 ```
 net-scan.exe ports -h 192.168.1.5 -p 5000 
 ```
## port scanner :
```
net-scan.exe ports -h 192.168.1.5 -p 5000 -th 300 -t 1
```

```
=> scans ports on 192.168.1.5:
    first 5000 ports
    with 300 threads
    1s timeout
```
## host discovery / scanner:
exemple :
```
net-scan.exe hosts -r 192.168.1-2.1-254 -p 445 -th 100 -t 5
```

```
=> scans from 192.168.1.1 to 192.168.2.254:
    with 100 threads
    from 192.168.135.1 to 168.235.254 
    port 445
    5 seconds timeout
```
less threads , more timeout = more accurate results


# release and usage :

drop the net-scan.exe on a machine you have a shell on and use the args above to start the scan.

i highly recommand compiling your own executables from code in VS :

compile with NET 3.5 for windows 7
and NET 4.5 for windows 10


BUT .. as some are lazy enough to not do that and willing to run a random .exe from the internet there's the release folder.
 
# todo list:
- clean the ugliness (made in few minutes in a hurry so sorry xD)
- scan from CIDR
- add port scan
- i'll add wtf