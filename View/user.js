$(document).ready(function () {
  const logInUser = JSON.parse(localStorage.getItem("logInUser"));
  // console.log(logInUser);
  $("#userName").text(logInUser.userName);

  ///Getting Categories from DB
  var categories;
  $.ajax({
    method: "GET",
    url: "http://localhost:54057/getallcategories",
    dataType: "json",
    success: function (data) {
      categories = data;
      displayCategories(categories);
    },
    error: function (error) {
      console.log(error);
    },
  });
  ////Displaying Categories
  function displayCategories(categories) {
    for (let i = 0; i < categories.length; i++) {
      var newOption = $(
        `<option id="${categories[i].category}"></option>`
      ).text(categories[i].category);
      $("#categorySelection").append(newOption);
    }
  }
  var selectedCategory;
  $("#categorySelection").on("change", function () {
    selectedCategory = $(this).val();
  });

  ///Displaying All Posts from DB
  var allPosts;
  var allComments;

  function fetchPosts(){
    return $.ajax({
      method: "GET",
      url: "http://localhost:54057/GetAllPosts",
      dataType: "json",
      success: function (data) {
        allPosts = data;
      },
      error: function (error) {
        console.log(error);
      },
    })
  }

  function fetchComments(){
    return $.ajax({
      method: "GET",
      url: "http://localhost:54057/GetAllComments",
      dataType: "json",
      success: function (data) {
        allComments = data;
      },
      error: function (error) {
        console.log(error);
      },
    })
  }
 
  $.when(fetchPosts(), fetchComments()).then(function () {
    DisplayPosts(allPosts);
  });

  function DisplayPosts(allPosts) {
    $("#blogContainer").empty();
    for (let i = 0; i < allPosts.length; i++) {
      // console.log(allPosts[i])

      $("#blogContainer").append(makeBlogCard(allPosts[i]));

      $(`#commentBtn${allPosts[i].postId}`).click(function () {
        const commentContainer = $(`#commentsContainer${allPosts[i].postId}`);
        if (commentContainer.css("display") === "none") {
          commentContainer.css("display", "flex");
        } else {
          commentContainer.css("display", "none");
        }
      });

      $(`#details${allPosts[i].postId}`).click(function () {
        const detailsContainer = $(`#detailsContainer${allPosts[i].postId}`);
        if (detailsContainer.css("display") === "none") {
          detailsContainer.css("display", "block");
        } else {
          detailsContainer.css("display", "none");
        }
      });

      $(`#commentForm${allPosts[i].postId}`).submit(function (event) {
        event.preventDefault();
        AddCommentToDB(allPosts[i].postId);
      });
      const commentsOfAPost = allComments.filter(
        (comment) => comment.postID == allPosts[i].postId
      );
      if (commentsOfAPost.length != 0) {
        for (let j = 0; j < commentsOfAPost.length; j++) {
          $(`#commentsContainer${allPosts[i].postId}`).append(
            displayCommentBox(commentsOfAPost[j])
          );
        }
      }

      $(`#delete${allPosts[i].postId}`).click((event) => {
        event.preventDefault();
        DeletePost(allPosts[i]);
      });
      $(`#update${allPosts[i].postId}`).click((event) => {
        event.preventDefault();
        UpdatePost(allPosts[i]);
      });
    }
  }

  
 

  function makeBlogCard(post) {
    let date = new Date(post.dateTime);
    let year = date.getFullYear();
    let month = date.getMonth();
    let day = date.getDate();
    return `
                <div class="blogCard">
                <h3 class="margin0">${post.Title}</h3>
                <p margin0>${post.Body}
                </p>
                <a class="details margin0" id="details${post.postId}">Details</a>
                <div class="personalBtn">
                    <button class="deleteBtn margin0" id="delete${post.postId}">Delete</button>
                    <button class="deleteBtn margin0" id="update${post.postId}">Update</button>
                </div>
                <div class="detailsContainer margin0" id="detailsContainer${post.postId}">
                    <p>Published By: <span>${post.userName}</span></p>
                    <p>Published on: <span>${day}-${month}-${year}</span></p>
                    <p>Category: <span>${post.category}</span></p>
                </div>
                <div>
                    <button class="commentBtn" id="commentBtn${post.postId}">Comments</button>
                </div>
                <div class="commentsContainer" id="commentsContainer${post.postId}">
                    <form class="commentForm" id="commentForm${post.postId}">
                        <input type="text" placeholder="Add Comment..." class="commentInput" id="comment${post.postId}" required>
                        <input type="submit" class="commentSubmitBtn">
                    </form>
                </div>
            </div> `;
  }
  function displayCommentBox(comment) {
    return `
            <div class="commentBox">
            <h4>${comment.userName}</h4>
            <p>${comment.comment}</p>
            </div>
              
    `;
  }

  ///Adding Comments
  function AddCommentToDB(postID) {
    const commentForm = {
      userID: logInUser.userId,
      userName: logInUser.userName,
      postID: postID,
      comment: $(`#comment${postID}`).val(),
    };
    $.ajax({
      type: "POST",
      url: "http://localhost:54057/AddComment",
      data: JSON.stringify(commentForm),
      contentType: "application/json",
      success: function (_data) {
        confirm("Comment Added Successfully!");
        $(`#commentsContainer${postID}`).append(
          `<div class="commentBox">
            <h4>${commentForm.userName}</h4>
            <p>${commentForm.comment}</p>
            </div>`
        );
        $(`#comment${postID}`).val("");
      },
      error: function (error) {
        confirm("Failed to Add comment due to " + error);
      },
    });
  }

  ///Adding a Blog
  $("#blogForm").submit(function (event) {
    event.preventDefault();
    if (!selectedCategory) {
      alert("Please select a category");
    } else {
      const blogData = {
        userID: logInUser.userId,
        userName: logInUser.userName,
        category: selectedCategory,
        Title: $("#Title").val(),
        Body: $("#Body").val(),
      };
      AddBlogToDB(blogData);
    }
  });
  function AddBlogToDB(blogData) {
    $.ajax({
      type: "POST",
      url: "http://localhost:54057/AddPost",
      data: JSON.stringify(blogData),
      contentType: "application/json",
      success: function (_data) {
        confirm("Your Blog has been Added Succeddfully!");
        $("#Title").val("");
        $("#Body").val("");
        $.when(fetchPosts(), fetchComments()).then(function () {
          DisplayPosts(allPosts);
        });
      },
      error: function (error) {
        confirm("Failed to Add Post due to " + error);
      },
    });
  }
  $("#commentBtn").click(function () {
    $("#commentsContainer").css("display", "flex");
  });
  ///LogOut
  $("#LogOutBtn").click(function () {
    window.location.href = "index.html";
  });

  $("#personalPosts").click(function () {
    displayPersonalPosts();
    $(".personalBtn").css("display", "block");
  });

  function displayPersonalPosts(){
    let personalPosts = allPosts.filter((post) => {
      return post.userID == logInUser.userId;
    });
    DisplayPosts(personalPosts);
  }


  $("#allPosts").click(function () {
    DisplayPosts(allPosts);
  });

  function DeletePost(post) {
    console.log(post);
    $.ajax({
      method: "Delete",
      url: `http://localhost:54057/DeletePost?postID=${post.postId}`,
      success: function () {
        alert("Post Successfully Deleted!");
      },
      error: function () {
        alert("There is an error with DeletePost!");
      },
    });
  }

  function UpdatePost(post) {
    $(`#categorySelection`).val(post.category);
    $("#Title").val(post.Title);
    $("#Body").val(post.Body);
    $(".update").css("display", "inline");

    $("#updateBtn").click(function (event) {
      event.preventDefault();
      selectedCategory = $("#categorySelection").val();
      const updateBlog = {
        postId: post.postId,
        category: selectedCategory,
        Title: $("#Title").val(),
        Body: $("#Body").val(),
      };
      $.ajax({
        method: "Put",
        url: "http://localhost:54057/UpdatePost",
        data: JSON.stringify(updateBlog),
        contentType: "application/json",
        success: function (_data) {
          console.log(_data);
          confirm("Your Blog has been updated Succeddfully!");
          $("#Title").val("");
          $("#Body").val("");
          // DisplayPosts(allPosts);
        },
        error: function (error) {
          confirm("Failed to Add Post due to " + error);
        },
      });
    });
  }
});
