const modalPlace = $("#modalPlace");
const notePlace = $("#notePlace");
var clickedButton;

const setHomePage = () => {
    notePlace.find("#parent").html('');
    //accordion yapisi
    //agac gorunumu
    $.ajax({
        type: "GET",
        url: "/Home/GetPreData",
        beforeSend: function () {
            notePlace.find("#parent").css('opacity','0.4');
        },
        success: function (data) {
            if (data) {
                data.forEach((d) => {
                    const html = `
                        <div class="accordion-item">
                            <h2 class="accordion-header d-flex justify-content-around align-items-center" id="flush-headingOne">
                                <button class="accordion-button collapsed"
                                        type="button"
                                        data-bs-toggle="collapse"
                                        data-bs-target="#main${d.id}"
                                        aria-expanded="false"
                                        aria-controls="flush-collapseOne">
                                    ${d.title}
                                </button>
                                <button class="btn btn-outline-primary mx-1 btnChildAdd">+</button>
                                <button class="btn btn-outline-danger btnMainDelete">x</button>
                            </h2>
                            <div id="main${d.id}" class="accordion-collapse collapse"
                                 aria-labelledby="flush-headingOne"
                                 data-bs-parent="#parent">
                                <div id="mainContent${d.id}" class="accordion-body">${d.content}</div>
                            </div>
                            </div>
                        </div>
        `;
                    notePlace.find("#parent").append(html);
                    let control = 0;
                    if (d.notesDal.length > 0) {
                        d.notesDal.forEach((dal) => {
                            if (control === 0) {
                                const childHtml = `
            <div class="accordion${dal.id} ms-1">
                                    <div class="accordion-item">
                            <h2 class="accordion-header d-flex justify-content-around align-items-center" id="flush-headingOne">
                                <button class="accordion-button collapsed"
                                        type="button"
                                        data-bs-toggle="collapse"
                                        data-bs-target="#child${dal.id}"
                                        aria-expanded="false"
                                        aria-controls="flush-collapseOne"
                                        id="${dal.baseId}">
                                    ${dal.title}
                                </button>
                                <button class="btn btn-outline-primary mx-1 btnChildAdd">+</button>
                                <button class="btn btn-outline-danger btnChildDelete">x</button>
                            </h2>
                            <div id="child${dal.id}" class="accordion-collapse collapse"
                                 aria-labelledby="flush-headingOne"
                                 data-bs-parent="#child">
                                <div class="accordion-body">${dal.content}</div>
                            </div>
                            </div>
                        </div>
            </div>
        `;
                                $("#parent").find(`#main${d.id}`).append(childHtml);
                                control++;
                            }
                            else {
                                if (dal.noteId === 0) {
                                    const childHtml = `
            <div class="accordion${dal.id} ms-1">
                                    <div class="accordion-item">
                            <h2 class="accordion-header d-flex justify-content-around align-items-center" id="flush-headingOne">
                                <button class="accordion-button collapsed"
                                        type="button"
                                        data-bs-toggle="collapse"
                                        data-bs-target="#child${dal.id}"
                                        aria-expanded="false"
                                        aria-controls="flush-collapseOne"
                                        id="${dal.baseId}">
                                    ${dal.title}
                                </button>
                                <button class="btn btn-outline-primary mx-1 btnChildAdd">+</button>
                                <button class="btn btn-outline-danger btnChildDelete">x</button>
                            </h2>
                            <div id="child${dal.id}" class="accordion-collapse collapse"
                                 aria-labelledby="flush-headingOne"
                                 data-bs-parent="#child">
                                <div class="accordion-body">${dal.content}</div>
                            </div>
                            </div>
                        </div>
            </div>
        `;
                                    $("#parent").find(`#main${d.id}`).append(childHtml);
                                    control++;
                                }
                                else {
                                    const childHtml = `
            <div class="accordion${dal.id} ms-2">
                                    <div class="accordion-item">
                            <h2 class="accordion-header d-flex justify-content-around align-items-center" id="flush-headingOne">
                                <button class="accordion-button collapsed"
                                        type="button"
                                        data-bs-toggle="collapse"
                                        data-bs-target="#child${dal.id}"
                                        aria-expanded="false"
                                        aria-controls="flush-collapseOne"
                                        id="${dal.baseId}">
                                    ${dal.title}
                                </button>
                                <button class="btn btn-outline-primary mx-1 btnChildAdd">+</button>
                                <button class="btn btn-outline-danger btnChildDelete">x</button>
                            </h2>
                            <div id="child${dal.id}" class="accordion-collapse collapse"
                                 aria-labelledby="flush-headingOne"
                                 data-bs-parent="#child">
                                <div class="accordion-body">${dal.content}</div>
                            </div>
                            </div>
                        </div>
            </div>
        `;
                                    $("#parent").find(`#child${dal.noteId}`).append(childHtml);
                                    control++;
                                }
                            }
                        })

                    }
                })
                notePlace.find("#parent").css('opacity', '1');
            }

        }
    })
}


$(document).ready(function () {
    
            //modal show yeni not icin
            $("#btnAdd").click(e => {
                e.preventDefault();
                $.ajax({
                    url: '/Home/Add',
                    type: "GET",
                    success: function (data) {
                        modalPlace.html(data);
                        modalPlace.find(".modal").fadeIn();
                    },
                    error: function (error) {
                        console.log(error)
                    }
                })
            });

    setHomePage();

            //modal close
            $(document).on('click', '.btnClose', function (e) {
                $.when(modalPlace.find('.modal').fadeOut())
                    .then(() => {
                        modalPlace.html('');
                    })
            })

    //var olan not icerisine yeni not eklemek icin acilacak modal
    $(document).on('click', '.btnChildAdd', function (e) {
        e.preventDefault();
        clickedButton = $(this);
        $.get("/Home/AddChild", function (data) {
            modalPlace.html(data);
            modalPlace.find(".modal").fadeIn();
        })
    })

})
