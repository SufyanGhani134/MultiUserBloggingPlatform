$(document).ready(function () {
  var allUsers;
  $.ajax({
    method: "GET",
    url: "http://localhost:54057/getallusers",
    contentType: "json",
    success: function (data) {
      allUsers = data;
      console.log(allUsers);
    },
    error: function (error) {
      console.log(error);
    },
  });
  $("#LogInForm").submit(function (event) {
    event.preventDefault();
    const FormData = {
      email: $("#email").val(),
      password: $("#password").val(),
    };
    logInValidation(FormData);
  });


  function logInValidation(FormData) {
    const logInUser = allUsers.find((user)=> FormData.email == user.email && FormData.password == user.password )
    console.log(logInUser)
    if(logInUser){
      alert(`Welcome ${logInUser.userName}`)
      localStorage.setItem("logInUser", JSON.stringify(logInUser))
      window.location.href ="user.html"
    }else{
      alert("Invalid Email or Password!")
    }
  }




  $("#LogInBtn").click(function () {
    $("#LogInForm").css("display", "block");
  });

  $("#closeBtn").click(function () {
    $("#LogInForm").css("display", "none");
  });
});
