module .

go 1.15

replace (
	"go-server" => "./hello-go-ipc/go-server"
	"go-client" => "./hello-go-ipc/go-client"
	"namedpipe" => "./hello-go-ipc/lib/namedpipe"
)