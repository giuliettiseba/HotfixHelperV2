# MilestoneUpdater
Update Milestone Recording Server From Remote PC 

Algorithm 

1) Use IP, username, domain, and password to establish a connection to the remote server 
2) Create a temp folder in the remote server 
3) Share the folder in the remote server 
4) Copy the file from the local machine to the remote server 
6) Create a process in the remote server with the copied file 
7) Wait until process execution finishes and show a message 
-----------------------------------------------------------------------------------------

Next steps.

A) Remote Server List.
1) connect to the MS and get the RS list
2) get RS list from the XML

B.1) Download Hotfix from download.milestonesys.com site 
1) Multiple Version support 
2) No internet? Manual map hotfix file from a directory 

C) Check that the hotfix was successfully installed



BUGS: 

1) After hotfix installation the trayicon is not visible but process is running. 
For security reasons the Win32_Process.Create method cannot be used to start an interactive process remotely. 
https://docs.microsoft.com/en-us/windows/win32/cimwin32prov/create-method-in-class-win32-process?redirectedfrom=MSDN




