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

function CreateProfileQuestion() {
    var self = new Object();

    self.Question = '';
    self.UserProfileAnswerId = 0;
    self.Answer = '';

    return self;
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

function CreateProfileQuizQuestion() {
    var self = new Object();
    self.Question = '';
    self.Answer = '';
    self.UserAnswer = '';
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
    self.SessionId = ko.observable("");
    self.LoginInfo = { UserName: ko.observable(""), Password: ko.observable("") };
    self.Register = { UserName: ko.observable(""), Password: ko.observable(""), Email: ko.observable("") };

        // color Questions
    self.CurrentQuestion = ko.observable(CreateQuestion());
    self.Questions = [];
    self.CorrectAnswers = ko.observable(0);

    self.ProfileQuestions = [];
    self.CurrentProfileQuestion = ko.observable(CreateProfileQuestion());
    self.QuestionIndex = 0;
    self.NextCaption = ko.observable("Next");

    self.ProfileQuizQuestions = [];
    self.CurrentProfileQuizQuestion = ko.observable(CreateProfileQuizQuestion());


    // Computed data

    // Operations

    self.NextQuizQuestion = function () {

        self.QuestionIndex = self.QuestionIndex + 1;

        if (self.QuestionIndex + 1 == self.ProfileQuizQuestions.length) {
            self.NextCaption("Finish");
        }
        else {
            self.NextCaption("Next");
        }


        if (self.QuestionIndex >= self.ProfileQuizQuestions.length) {

            self.CorrectAnswers(0);
            for (var i = 0; i < self.ProfileQuizQuestions.length; i++) {
                if (self.ProfileQuizQuestions[i].UserAnswer) {
                    if (self.ProfileQuizQuestions[i].Answer.toLowerCase() == self.ProfileQuizQuestions[i].UserAnswer.toLowerCase()) {
                        self.CorrectAnswers(self.CorrectAnswers() + 1);
                    }
                }
            }

            $.mobile.changePage("#ColorGameScores",
                                    {
                                        transition: "slide",
                                        allowSamePageTransition: true,
                                        changeHash: false
                                    }
                            );
            $('#ColorGameScores').trigger('create');
            self.SaveResults(2, self.ProfileQuizQuestions.length);
            //self.SaveResults();
        }
        else {
            self.CurrentProfileQuizQuestion(self.ProfileQuizQuestions[self.QuestionIndex]);
            $.mobile.changePage("#ProfileQuiz",
                                    {
                                        transition: "flip",
                                        allowSamePageTransition: true,
                                        changeHash: false
                                    }
                            );
            $('#ProfileQuiz').trigger('create');

        }

    }

    self.GetProfileQuiz = function () {
        try {

            ShowLoading('Loading...');
            $('#loginPage').addClass('ui-disabled');

            $.ajax({
                type: 'POST',
                url: URIHOME + GAMES + 'FetchRandomUsreProfileQuizQuestions',
                data: { SessionId: self.SessionId(), QuestionCount: 10 },
                dataType: 'json',
                success: function (data) {
                    HideLoading();
                    $('#loginPage').removeClass('ui-disabled');

                    if (!data.Success) {
                        self.Error(data.Error);
                    }
                    else {

                        self.QuestionIndex = 0;
                        self.ProfileQuizQuestions = data.QuestionList;
                        self.CurrentProfileQuizQuestion(self.ProfileQuizQuestions[0]);

                        $.mobile.changePage("#ProfileQuiz",
                            {
                                transition: "slide"
                            }
                        );

                        $('#ProfileQuiz').trigger('create');

                        self.startTime = new Date();
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
    };

    self.SaveProfileAnswers = function () {
        $.ajax({
            type: 'POST',
            url: URIHOME + GAMES + 'SaveUserProfileQuestions',
            data: {
                QuestionAnswerList: JSON.stringify(self.ProfileQuestions), SessionId: self.SessionId()
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

    //Select Next Question
    self.SelectNextProfileQuestion = function () {

        self.QuestionIndex = self.QuestionIndex + 1;

        if (self.QuestionIndex + 1 == self.ProfileQuestions.length) {
            self.NextCaption("Finish");
        }
        else {
            self.NextCaption("Next");
        }


        if (self.QuestionIndex >= self.ProfileQuestions.length) {
            $.mobile.changePage("#profileFinish",
                                    {
                                        transition: "slide",
                                        allowSamePageTransition: true,
                                        changeHash: false
                                    }
                            );
            $('#profileFinish').trigger('create');
            self.SaveProfileAnswers();
            //self.SaveResults();
        }
        else {
            self.CurrentProfileQuestion(self.ProfileQuestions[self.QuestionIndex]);
            $.mobile.changePage("#profileQuestions",
                                    {
                                        transition: "flip",
                                        allowSamePageTransition: true,
                                        changeHash: false
                                    }
                            );
            $('#profileQuestions').trigger('create');

        }


    }


    self.NextProfile = function () {
        self.SelectNextProfileQuestion();
    }

    self.BackProfile = function () {
        if (self.QuestionIndex > 0) {
            self.QuestionIndex = self.QuestionIndex - 1;

            if (self.QuestionIndex + 1 == self.ProfileQuestions.length) {
                self.NextCaption("Finish");
            }
            else {
                self.NextCaption("Next");
            }

            self.CurrentProfileQuestion(self.ProfileQuestions[self.QuestionIndex]);
            $.mobile.changePage("#profileQuestions",
                                    {
                                        transition: "flip",
                                        allowSamePageTransition: true,
                                        changeHash: false
                                    }
                            );
            $('#profileQuestions').trigger('create');
        }
    }

    self.GetProfileQuestions = function () {

        try {

            ShowLoading('Loading...');
            $('#loginPage').addClass('ui-disabled');

            $.ajax({
                type: 'POST',
                url: URIHOME + GAMES + 'FetchUserProfileQuestions',
                data: { SessionId: self.SessionId() },
                dataType: 'json',
                success: function (data) {
                    HideLoading();
                    $('#loginPage').removeClass('ui-disabled');

                    if (!data.Success) {
                        self.Error(data.Error);
                    }
                    else {

                        self.QuestionIndex = 0;
                        self.ProfileQuestions = data.QuestionList;
                        self.CurrentProfileQuestion(self.ProfileQuestions[0]);

                        $.mobile.changePage("#profileQuestions",
                            {
                                transition: "slide"
                            }
                        );

                        $('#profileQuestions').trigger('create');

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

    self.SaveResults = function (GameType, TotalQuestions) {
        self.endTime = new Date();
        var elapsed = (vw.endTime - vw.startTime) / 1000
        self.duration(elapsed);
        $.ajax({
            type: 'POST',
            url: URIHOME + GAMES + 'InsertGameResult',
            data: {
                UserName: self.LoginInfo.UserName(), Password: self.LoginInfo.Password(), gameType: GameType,
                Score: self.CorrectAnswers(), TestDuration: elapsed, Total: TotalQuestions
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

            self.SaveResults(1, self.Questions.length);
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
                        self.SessionId(data.SessionId);
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
                        self.SessionId(data.SessionId);
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


