# RGBSync

This project is an attempt to synchronise the RGB colours of Gigabyte and Corsair components. It sets the color to Cyan if the temperature is below 60C, then smoothly tranisitions to Red if the temperature goes above 60C. Here are some of components it can change the colours of:

 - Gigabyte Aorus graphics card
 - Non-addressable/Addressable LED strip connected to the Gigabyte motherboard headers (D_LED/LED_C).
 - Corsair Vengeance RGB pro, H150i Pro, Lighting Node Pro

And possibly more.

# Using this project
The code has been uploaded here for reference purposes, to help someone who is trying to control the colours in a programmatic way. However, you should still be able to run this project in Visual Studio 2019.

## Dependencies

 1. You need an Nvidia GPU. If you are using an AMD GPU, you will need to make some changes to the code for it to work. To do that, please create an issue and I will respond.
 2. Make sure you have Corsair iCUE (v3.30.89) and RGB Fusion (vB20.0529.1) installed.

## Running
Follow these steps to run the project:

 1. Once you have cloned this repository, open Visual Studio 2019 with Administrator privileges. RGB Fusion SDK and it's dependencies need this privilege to function.
 2. To build the solution, right click on the RGBSync solution and click build solution.
	 - The project assumes that RGB Fusion is installed in Program Files (x86).
  3. Make sure that you are using the x86 configuration. You can find it right beside the  green "Start" button.
  4. Start the project by clicking "Start" button.

## Troubleshoot
Please create an issue if you need any help with the code or you are unable to run the project. I will be happy to help.
