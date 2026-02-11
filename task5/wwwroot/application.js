let page = 1

const grid = document.getElementById("grid")
const localeEl = document.getElementById("locale")
const seedEl = document.getElementById("seed")
const likesEl = document.getElementById("likes")
const likesValue = document.getElementById("likesValue")
const pageNum = document.getElementById("pageNum")

document.getElementById("randomSeed").onclick = () => {
    seedEl.value = Math.floor(Math.random() * 100000000)
    reload()
}

document.getElementById("prev").onclick = () => {
    if (page > 1) {
        page--
        reload()
    }
}

document.getElementById("next").onclick = () => {
    page++
    reload()
}

localeEl.onchange = reload
seedEl.oninput = reload

likesEl.oninput = () => {
    likesValue.innerText = likesEl.value
}

likesEl.onchange = reloadLikes

function buildUrl() {
    return `/api/songs?seed=${seedEl.value}&page=${page}&locale=${localeEl.value}&likes=${likesEl.value}`
}

function reload() {
    grid.innerHTML = ""
    pageNum.innerText = page
    load()
}

function reloadLikes() {
    fetch(buildUrl())
        .then(r => r.json())
        .then(data => {
            data.forEach(s => {

                const likeCell = document.getElementById(`likes-${s.index}`)
                if (likeCell) likeCell.innerText = s.likes

                const likeDetail = document.getElementById(`likes-detail-${s.index}`)
                if (likeDetail) likeDetail.innerText = s.likes
            })
        })
}

function load() {
    fetch(buildUrl())
        .then(r => r.json())
        .then(render)
}

function render(data) {

    let html = `
        <table class="songs-table">
            <thead>
                <tr>
                    <th>#</th>
                    <th>Title</th>
                    <th>Artist</th>
                    <th>Album</th>
                    <th>Genre</th>
                    <th>Likes</th>
                </tr>
            </thead>
            <tbody>
    `

    data.forEach(s => {
        html += `
            <tr class="song-row"
                onclick="toggleDetails(${s.index},
                '${encodeURIComponent(s.album)}',
                '${encodeURIComponent(s.artist)}',
                '${encodeURIComponent(s.title)}',
                '${encodeURIComponent(s.genre)}')">

                <td>${s.index}</td>
                <td>${s.title}</td>
                <td>${s.artist}</td>
                <td>${s.album}</td>
                <td>${s.genre}</td>
                <td id="likes-${s.index}">${s.likes}</td>
            </tr>

            <tr id="details-${s.index}" class="details hidden">
                <td colspan="6">
                    <div class="details-content">

                        <img id="cover-${s.index}" class="cover-fixed">

                        <div class="info">

                            <div class="meta">
                                <div><b>Title:</b> ${s.title}</div>
                                <div><b>Artist:</b> ${s.artist}</div>
                                <div><b>Album:</b> ${s.album}</div>
                                <div><b>Genre:</b> ${s.genre}</div>
                                <div><b>Likes:</b> <span id="likes-detail-${s.index}">${s.likes}</span></div>
                            </div>

                            <audio controls id="audio-${s.index}"></audio>

                        </div>

                    </div>
                </td>
            </tr>
        `
    })

    html += "</tbody></table>"
    grid.innerHTML = html
}

function toggleDetails(index, album, artist, title, genre) {

    const row = document.getElementById(`details-${index}`)
    const hidden = row.classList.contains("hidden")

    row.classList.toggle("hidden")

    if (hidden) {

        document.getElementById(`cover-${index}`).src =
            `/api/cover?seed=${seedEl.value}&page=${page}&index=${index}&locale=${localeEl.value}&album=${album}&artist=${artist}`

        document.getElementById(`audio-${index}`).src =
            `/api/audio?seed=${seedEl.value}&page=${page}&index=${index}`

        const currentLikes = document.getElementById(`likes-${index}`).innerText
        document.getElementById(`likes-detail-${index}`).innerText = currentLikes
    }
}

reload()