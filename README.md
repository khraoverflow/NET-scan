# NET-scan
.NET cli multi threaded host discovery tool (and port scanner ... at some point ..eventually).

scans a subnet range for a specific port and report hosts having that port open
 
 simple exemple command:
 ```
 khra-scan.exe hosts -r 192.168.1.1-254 -p 445
 ```

exemple :
```
khra-scan.exe hosts -r 192.168.1-2.1-254 -p 445 -th 100 -t 5
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

drop the khra-scan.exe on a machine you have a shell on and use the args above to start the scan.

i highly recommand compiling your own executables from code in VS :

compile with NET 3.5 for windows 7
and NET 4.5 for windows 10


BUT .. as some are lazy enough to not do that and willing to run a random .exe from the internet there's the release folder.
 
# todo list:
- clean the ugliness (made in few minutes in a hurry so sorry xD)
- scan from CIDR
- add port scan
- fix compareExchange