
-> StringToNumberConverter is main class that holds the logic to check if the string is valid and converts to numbers
-> It has two public functions:
		IsValidNumber - Checks if the string is valid
			TODO WordsInValidOrder function which would check the order of words to make sure the text is a valid number
			TODO The validation logic can be abstracted to simple IValidate interface and have few validators run through the input reducing the responsibility
				of the StringToNumberConverter class


		ConvertTo - Converts the given string to the number format
			This function does some input validation and then finds the place values aka scale or the power of 10 the number needs to mulplied
			It then breaks the word array on the index of those place values and makes recursive call to compute the value in each half and sum them
		
		ConvertTo- Another overload of this function converts a number to it's word format.
					Limited to number of size less than 100 million
	
	Known Issues or Items to Enhance:
		It doesn't accept the number in certain format like with -. For example "twenty-four"
		It's doesn't auto correct the spelling and infer the words
		The order of the words has be correct, it doesn't yet validate that
		The function ConvertTo which would convert number to it's text form is limited to number of size less than 100 Million. 

