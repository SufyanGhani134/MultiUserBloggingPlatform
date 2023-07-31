$(document).ready(function () {
  var allUsers;
  $.ajax({
    method: "GET",
    url: "http://localhost:54057/getallusers",
    contentType: "json",
    success: function (data) {
      allUsers = data;
      // console.log(allUsers);
    },
    error: function (error) {
      console.log(error);
    },
  });

  $("#SignUpForm").submit(function (event) {
    console.log("Entering")
    event.preventDefault();
    let isError = false;
    let password = $("#password").val();
    let confirmPassword = $("#confirmPassword").val();
    const findUser = allUsers.find((user) => user.email == $("#email").val());
    console.log(findUser)
    try {
      if (password != confirmPassword)
        throw "Passwords Do not match, Try Again!";
      if (findUser ) throw "Email already registered...";
    } catch (error) {
      isError = true;
      console.log(error, findUser)
      alert(error);
    }

    if (!isError) {
      const FormData = {
        userName: $("#userName").val(),
        email: $("#email").val(),
        password: $("#password").val(),
      };

      $.ajax({
        method: "POST",
        url: "http://localhost:54057/SignUp",
        data: JSON.stringify(FormData),
        contentType: "application/json",
        success: function (_data) {
          confirm("User Sign Up successfully!");
          $("#userName").val("")
          $("#email").val("")
          $("#password").val("")
          $("#confirmPassword").val("")

        },
        error: function (error) {
          confirm(
            "Failed to Sign Up because of: " + error + ". Please try again!"
          );
        },
      });
    }
  });

  $("#LogInAnchor").click(function () {
    $("#LogInForm").css("display", "block");
  });

  $("#closeBtn").click(function () {
    $("#LogInForm").css("display", "none");
  });

  $("#LogInForm").submit(function (event) {
    event.preventDefault();
    const logInFormData = {
      email: $("#logInEmail").val(),
      password: $("#logInPassword").val(),
    };
    logInValidation(logInFormData);
  });


  function logInValidation(logInFormData) {
    const logInUser = allUsers.find((user)=> logInFormData.email == user.email && logInFormData.password == user.password )
    console.log(logInUser)
    if(logInUser){
      alert(`Welcome ${logInUser.userName}`)
      localStorage.setItem("logInUser", JSON.stringify(logInUser))
      window.location.href ="user.html"
    }else{
      alert("Invalid Email or Password!")
    }
  }
});
