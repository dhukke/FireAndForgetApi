
# Endpoints Overview

## `addWithDelay`
This endpoint triggers an `ItemAddedEvent` and **waits for the event processing to complete** before returning a response to the caller. This means the HTTP request remains open until all related operations (such as database updates, notifications, etc.) are finished. This approach ensures that the client receives confirmation only after all work is done, but it can increase response time, especially if the event processing is slow or involves external dependencies.

## `addWithFireAndForget`
This endpoint triggers an `ItemAddedFireAndForgetEvent` using a **fire-and-forget** approach. It immediately returns a response to the caller, while the event is handled asynchronously in the background. The HTTP request is released as soon as the event is queued, not when it is processed.

### Internal Implementation
- The fire-and-forget pattern is implemented using an in-memory message queue and background job processor (`FireAndForgetBus` and related classes).
- When the endpoint is called, the event is published to the queue, and the HTTP response is sent back right away.
- A background job picks up the event and processes it independently of the HTTP request lifecycle.

### Benefits of Fire-and-Forget
- **Faster HTTP Responses:** The client receives a response almost instantly, improving perceived performance and user experience.
- **Decoupling:** The API is decoupled from the event processing logic, making it more resilient to slow or unreliable downstream systems.
- **Scalability:** Background processing can be scaled independently, and failures can be retried without impacting the client.
- **Resource Efficiency:** Frees up web server threads quickly, allowing higher throughput under load.

**Use fire-and-forget when you want to acknowledge receipt of a request quickly and do not require the client to wait for all processing to finish.**

