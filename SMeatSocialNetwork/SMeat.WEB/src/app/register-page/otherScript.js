var other = document.getElementById("otherToCheck");
var text = document.getElementById("genderText");
var buttons = document.getElementsByName("gender");

other.addEventListener("click", function(){
	show();	
});

for(var j = 0; j < buttons.length; j++){
	if(buttons[j] != other){
		buttons[j].addEventListener("click", function(){
    			hide();
		});
	}
}

var show = function()
{
        text.style.display = 'inline';
}

var hide = function()
{
        text.style.display = 'none';
} 