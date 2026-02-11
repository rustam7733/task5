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

likesEl.onchange = () => {
    reloadLikes()
}


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
                const likeEl = document.getElementById(`likes-${s.index}`)
                if (likeEl)
                    likeEl.innerText = `Likes: ${s.likes}`
            })
        })
}

function load() {
    fetch(buildUrl())
        .then(r => r.json())
        .then(render)
}

function render(data) {
    grid.innerHTML = ""

    data.forEach(s => {
        const div = document.createElement("div")
        div.className = "card"

        div.innerHTML = `
            <img src="/api/cover?seed=${seedEl.value}&page=${page}&index=${s.index}&locale=${localeEl.value}&album=${encodeURIComponent(s.album)}&artist=${encodeURIComponent(s.artist)}">

            <div class="meta">
                <div class="title">${s.title}</div>
                <div class="artist">Artist: ${s.artist}</div>
                <div class="album">Album: ${s.album}</div>
                <div class="likes" id="likes-${s.index}">Likes: ${s.likes}</div>
            </div>

            <audio controls src="/api/audio?seed=${seedEl.value}&page=${page}&index=${s.index}"></audio>
        `
        grid.appendChild(div)
    })
}

reload()