function setSurveyInfo() {
    $("#myModal").modal("hide");
    surveyTitle = $("#input_surveyTitle").val();
    surveyInfo = $("#input_surveyInfo").val();
    $("#surveyTitle").html(surveyTitle);
    $("#surveyInfo").html(surveyInfo);
}


function addOneQuestion(type) {

    if ($(".question-editing").length > 0) {
        alert("请先保存或者删除未保存的问题！");
        return;
    }

    var oneQuestion = $("#question-exmple").clone();

    questionCount++;

    oneQuestion.attr("id", "question-div-" + questionCount);

    oneQuestion.addClass("question-editing");
    oneQuestion.css("display", "block");
    oneQuestion.attr("question-id", questionCount);
    oneQuestion.attr("question-type", type);

    //oneQuestion.select("")

    oneQuestion.find(".question-type-select").val(type); //val(type);

    oneQuestion.find(".question-title").html("问题" + questionCount);

    $("#surveyPage").append(oneQuestion);

}

function deleteOneQuestion(item) {

    var status = confirm("该操作将会删除该题目的所有内容，是否继续操作?");


    if (!status) {
        return;
    }

    var questionDiv = $(item).parent().parent().parent().parent();
    var list = questionDiv.parent();
    //list.removeChild(questionDiv);
    questionDiv.remove();
    questionCount--;

    var questionDivs = $(".question");
    for (var i = 0; i < questionDivs.length; i++) {
        questionDiv = questionDivs.eq(i).first();
        questionDiv.find(".question-title").html("问题" + (i+1));
        questionDiv.find(".pre-question-num").html((i+1) + "、");
        questionDiv.attr("question-id", (i+1));
    }


}

var types = ["blank", "multi", "single"];

function saveOneQuestion(item) {

    var questionTitle = $(".question-editing .input-question-title").val();

    var questionDiv = $(".question-editing");

    var questionId = questionDiv.attr("question-id");
    var type = questionDiv.find(".question-type-select").val();
    if (questionTitle.length < 1) {
        showErrorMsg(questionDiv, "问题不能为空!");
        return;
    }


    var optionInputs = questionDiv.find(".question-editor-option");


    var questionOptions = new Array(optionInputs.length);

    var option;
    //alert(type);
    if (type !== 'blank')
        for (var i = 0; i < optionInputs.length; i++) {
            option = optionInputs.eq(i).first().val();
            console.log(option);
            if (option.length < 1) {
                showErrorMsg(optionInputs.eq(i).first(), "选项不能为空!");
                return;
            }
            questionOptions[i] = option;
        }


    //alert(questionOptions[0]);

    //alert(optionInputs.length);


    if (questionDiv.find(".question-require-checkbox").is(":checked")) {
        questionDiv.find(".pre-question-tag").css("display", "inline").attr("data", 'Y');
    } else {
        questionDiv.find(".pre-question-tag").css("display", "none").attr("data", 'N');
    }

    questionDiv.removeClass("question-editing").addClass("question-completed");

    questionDiv.find(".question-preview .pre-question-title").html(questionTitle);

    questionDiv.find(".pre-question-num").html(questionId + "、");

    var preQuestionOptionDiv = questionDiv.find(".pre-question-options");


    questionDiv.find(".pre-question-tag").attr("q-type", types.indexOf(type));


    console.log(type);
    $(preQuestionOptionDiv).empty();
    
    if (type === "blank") {
        preQuestionOptionDiv.append("<input type=\"text\" value=\"\"/>");
    } else if (type === "multi") {
        for (option in questionOptions) {
            preQuestionOptionDiv.append("<li><input type=\"checkbox\"/>" +
                questionOptions[option] +
                "</br></li>");

        }
    } else {
        for (option in questionOptions) {
            preQuestionOptionDiv.append(
                "<li><input type=\"radio\"/>" + questionOptions[option] + "</br></li>");
        }
    }


    //alert(questionTitle);


}
function editQuestion(item) {
    if ($(".question-editing").length > 0) {
        alert("请先保存或者删除未保存的问题！");
        return;
    }

    var questionDiv=$(item).parent().parent().parent().parent();

    questionDiv.removeClass("question-completed").addClass("question-editing");



}

function addOption(item) {
    var oldOption = item.parentNode;
    var newOption = oldOption.cloneNode(true);
    oldOption.parentNode.insertBefore(newOption, oldOption.nextSibling);
    resetOptionBtn();
}

function moveOptionUp(item) {
    var oldOption = item.parentNode;
    var upOption = oldOption.previousSibling;
    upOption.parentNode.removeChild(oldOption);
    upOption.parentNode.insertBefore(oldOption, upOption);
    resetOptionBtn();
}

function moveOptionDown(item) {
    var oldOption = item.parentNode;
    var downOption = oldOption.nextSibling;
    downOption.parentNode.removeChild(oldOption);
    downOption.parentNode.insertBefore(oldOption, downOption.nextSibling);
    resetOptionBtn();
}

function removeOption(item) {
    var oldOption = item.parentNode;
    if (oldOption.parentNode.childElementCount < 3) {
        showErrorMsg(oldOption, "至少需要两个选项!");
        return;
    }
    oldOption.parentNode.removeChild(oldOption);
    resetOptionBtn();
}

function changeQuestionType(item) {
    var questionDiv = item.parentNode.parentNode.parentNode;
    questionDiv.setAttribute("question-type", item.options[item.selectedIndex].value);
    //questionDiv.attr("question-type", item.options[item.selectedIndex].value);
    console.log(item.NodeSelector);
}

function resetOptionBtn(item) {
    var list = $(".question-editing .options-editor ul li");
    console.log(list.length);
    list.first().find(".btn-up").attr("disabled", "disabled");
    list.first().find(".btn-down").removeAttr("disabled");


    list.last().find(".btn-down").attr("disabled", "disabled");
    list.last().find(".btn-up").removeAttr("disabled");

    for (var i = 1; i < list.length - 1; i++) {
        list.eq(i).first().find(".btn").removeAttr("disabled");
    }

}


function showErrorMsg(item, msg) {
    $(".question > .question-editor > .error-msg").html(msg);
    setTimeout(function() {
            $(".question > .question-editor > .error-msg").html("");
        },
        2000);
    //var nn=new SKclass(item,20,70);
    shakeItem(item, 8, 10, 100);
}