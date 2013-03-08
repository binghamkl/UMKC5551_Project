var GAMES = "Games/";

function ShowLoading(message) {
    $.mobile.loading('show', {
        text: message,
        textVisible: true,
        theme: 'b',
        html: ''
    });
}

function HideLoading() {
    $.mobile.loading('hide', {});
}

function CreateQuestion() {
    var self = new Object();
    self.LeftWord = '';
    self.LeftColor = '';
    self.RightWord = '';
    self.RightColor = '';
    self.IsMatch = true;
    self.UserCorrect = false;
    return self;
}


// Overall viewmodel for this screen, along with initial state
function AgileMindViewModel() {
    var self = this;

    // Non-editable catalog data - would come from the server
    self.startTime = new Date();
    self.endTime = new Date();

    // Editable data

    self.IsLoggedIn = ko.observable(false);
    self.Error = ko.observable("");
    self.duration = ko.observable(0);

    self.LoginInfo = { UserName: ko.observable(""), Password: ko.observable("") };
    self.Register = { UserName: ko.observable(""), Password: ko.observable(""), Email: ko.observable("") };

        // color Questions
    self.CurrentQuestion = ko.observable(CreateQuestion());
    self.Questions = [];
    self.CorrectAnswers = ko.observable(0);

    // Computed data

    // Operations

    self.SaveResults = function () {
        self.endTime = new Date();
        var elapsed = (vw.endTime - vw.startTime) / 1000
        self.duration(elapsed);
        $.ajax({
            type: 'POST',
            url: URIHOME + GAMES + 'InsertGameResult',
            data: {
                UserName: self.LoginInfo.UserName(), Password: self.LoginInfo.Password(), gameType: 1,
                Score: self.CorrectAnswers(), TestDuration: elapsed, Total: self.Questions.length
            },
            dataType: 'json',
            success: function (data) {

                if (!data.Success) {
                    self.Error(data.Error);
                }
                else {

                    self.Error('');
                }

            },
            error: function (jqXHR, textStatus, errorThrown) {
                self.Error(errorThrown);
            }

        });
    };

        //CreateQuestions
    self.CreateQuestions = function () {

        try {

            ShowLoading('Loading...');
            $('#loginPage').addClass('ui-disabled');

            $.ajax({
                type: 'POST',
                url: URIHOME + GAMES + 'CreateColorGame',
                data: {},
                dataType: 'json',
                success: function (data) {
                    HideLoading();
                    $('#loginPage').removeClass('ui-disabled');

                    if (!data.Success) {
                        self.Error(data.Error);
                    }
                    else {

                        self.Questions = data.Questions
                        self.CurrentQuestion(self.Questions[0]);
                        self.CorrectAnswers(0);

                        $.mobile.changePage("#ColorGame",
                            {
                                transition: "slide"
                            }
                        );
                        self.startTime = new Date();

                        $('#ColorGame').trigger('create');

                        self.Error('');
                    }

                },
                error: function (jqXHR, textStatus, errorThrown) {
                    HideLoading();
                    $('#loginPage').removeClass('ui-disabled');
                    self.Error(errorThrown);
                }

            });


        } catch (e) {
            HideLoading();
            $('#loginPage').removeClass('ui-disabled');
            self.Error(e.message);
        }
    }




    //UserSelect Match
    self.Match = function () {
        if (self.CurrentQuestion().IsMatch == true) {
            self.UserCorrect = true;
            self.CorrectAnswers(self.CorrectAnswers() + 1);
        }
        else {
            self.UserCorrect = false;
        }
        self.SelectNextQuestion();

    };

    //User selected nomatch
    self.NoMatch = function () {
        if (self.CurrentQuestion().IsMatch == true) {
            self.UserCorrect = false;
        }
        else {
            self.UserCorrect = true;
            self.CorrectAnswers(self.CorrectAnswers() + 1);
        }
        self.SelectNextQuestion();
    };

    //Select Next Question
    self.SelectNextQuestion = function () {

        var foundQuestion = false;

        //cycle through questions find the current one, then select the next one.
        for (var questionIndex = 0; questionIndex < self.Questions.length; questionIndex++) {
            if (foundQuestion == true) {
                self.CurrentQuestion(self.Questions[questionIndex]);
                foundQuestion = false;
                break;
            }
            if (self.CurrentQuestion() == self.Questions[questionIndex])
                foundQuestion = true;
        }

        if (foundQuestion == true) {            // That's all the questions
            $.mobile.changePage("#ColorGameScores",
                                    {
                                        transition: "slide",
                                        allowSamePageTransition: true,
                                        changeHash: false
                                    }
                            );
            $('#ColorGameScores').trigger('create');

            self.SaveResults();
        }
        else {
            $.mobile.changePage("#ColorGame",
                                    {
                                        transition: "flip",
                                        allowSamePageTransition: true,
                                        changeHash: false
                                    }
                            );
            $('#ColorGame').trigger('create');
        }

    }


    //Login
    self.Login = function () {
        try {

            ShowLoading('Loading...');
            $('#loginPage').addClass('ui-disabled');

            $.ajax({
                type: 'POST',
                url: URIHOME + USER + 'LoginUser',
                data: { UserName: self.LoginInfo.UserName(), Password: self.LoginInfo.Password() },
                dataType: 'json',
                success: function (data) {
                    HideLoading();
                    $('#loginPage').removeClass('ui-disabled');

                    if (!data.Success) {
                        self.IsLoggedIn(false);
                        self.Error(data.Error);
                    }
                    else {
                        self.IsLoggedIn(true);
                        self.Error('');
                    }

                },
                error: function (jqXHR, textStatus, errorThrown) {
                    HideLoading();
                    $('#loginPage').removeClass('ui-disabled');
                    self.IsLoggedIn(false);
                    self.Error(errorThrown);
                }

            });


        } catch (e) {
            HideLoading();
            $('#loginPage').removeClass('ui-disabled');
            self.Error(e.message);
            self.IsLoggedIn(false);
        }
    }


    //Login
    self.RegisterUser = function () {
        try {

            ShowLoading('Loading...');
            $('#CreateUser').addClass('ui-disabled');

            $.ajax({
                type: 'POST',
                url: URIHOME + USER + 'CreateUser',
                data: { UserName: self.Register.UserName(), Password: self.Register.Password(), Email: self.Register.Email() },
                dataType: 'json',
                success: function (data) {
                    HideLoading();
                    $('#CreateUser').removeClass('ui-disabled');

                    if (!data.Success) {
                        self.IsLoggedIn(false);
                        self.Error(data.Error);
                    }
                    else {
                        self.IsLoggedIn(true);
                        self.LoginInfo.UserName(self.Register.UserName());
                        self.LoginInfo.Password(self.Register.Password());
                        self.Error('');
                        $("#CreateUser").dialog("close");
                    }

                },
                error: function (jqXHR, textStatus, errorThrown) {
                    HideLoading();
                    $('#CreateUser').removeClass('ui-disabled');
                    self.IsLoggedIn(false);
                    self.Error(errorThrown);
                }

            });


        } catch (e) {
            HideLoading();
            $('#CreateUser').removeClass('ui-disabled');
            self.Error(e.message);
            self.IsLoggedIn(false);
        }
    }


}


