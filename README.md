# Wwise-Unity-Starter-Template

Unity template which allows composers to test Wwise Projects

Optimized for Unity Version:
2019.3.9f1

Optimized for Wwise Version: 
2019.2.0.7216

Video Tutorials:
https://www.youtube.com/watch?v=NVyCF5SY63o
https://www.youtube.com/watch?v=1i0SBLdZbZg

READ BELOW FOR MAC OS CATALINA USERS ONLY:
Make sure you've applied the updates for the latest version 10.15.5 with the supplementary
update.  In addition Catalina has security settings which don't allow Unity to properly 
load Wwise soundbanks. To bypass the security precautions that the Mac OS has put in place 
you need to type the following line into the Terminal: 

sudo spctl --master-disable

Followed by your computer password to complete the command. This does leave your computer 
more open to hacker attacks, so do this at your own risk, be careful and consider yourself 
appropriately warned.  

Then when finished working on the project, 
restore the security settings with the following Terminal command:

sudo spctl --master-enable 

Note that you'll need to also enter your computer password after the command in order to
complete it.
