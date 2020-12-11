package main // import "go-client2"

import (
	"bufio"
	"fmt"
	"os"

	"namedpipe"
)

// Message - 메시지
type Message struct {
	Message string
}

// SendAndReceiveMessage - 주고받기
func SendAndReceiveMessage() {
	fmt.Println("Connecting to server...")

	pipeName := "helloNamedPipe"
	pipeArgs := []string{}

	pipe, err := namedpipe.NewNamedPipeClient(pipeName, pipeArgs...)
	if err != nil {
		fmt.Printf("Error connecting to named pipe %s - %v", pipeName, err)
		os.Exit(1)
	}
	fmt.Println("Connected to server")

	reader := bufio.NewReader(os.Stdin)

	for {
		// fmt.Print("-> ")
		text, _ := reader.ReadString('\n')

		writer, err := pipe.GetWriter()
		if err != nil {
			fmt.Printf("Error in getting pipe writer %v", err)
			os.Exit(1)
		}

		w := bufio.NewWriter(writer)
		_, err = w.WriteString(text)
		if err != nil {
			fmt.Printf("Error in writing to pipe %v", err)
			os.Exit(1)
		}

		err = w.Flush()
		if err != nil {
			fmt.Printf("Error in flushing pipe %v", err)
			os.Exit(1)
		}

	}
}

func main() {
	fmt.Println("Created client")
	SendAndReceiveMessage()
}
