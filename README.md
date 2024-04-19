BT Code Crafters OTP Challenge Backend is an app that generates OTP's for the user.
 I explained the flow in the FE Readme. I will explain the functionalities of the BE:
  There is a GET for generating the OTP, a POST for verifying if the OTP matches with the 
  correct one. Also, I encrypt the data before transmiting it (both ways is the same ideea).
  There is a timer in which you generate an OTP and you cannot generate another one until this one expires (I check if there is already an OTP).

I saved the OTP decrypted in a file (the encryption is used to secure the transport of data). This can be extended to a database if there is more data to store (in a secure way), but for this exercise I considered this method to be good enough. 

The Unit Testing is done in another project situated in the BE project, and it verifies
 the outputs generated from the GET and POST methods. The tests passed, so there are
 no problem with those 2 methods.

Project developed in C# with .NET 8.0. (With VisualStudio 2022)