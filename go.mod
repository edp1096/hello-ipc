module .

go 1.15

replace (
	"go-server" => "./hello-go-ipc/go-server"
	"go-server2" => "./hello-go-ipc/go-server2"
	"go-client" => "./hello-go-ipc/go-client"
	"go-client2" => "./hello-go-ipc/go-client2"
	"namedpipe" => "./hello-go-ipc/lib/namedpipe"
)