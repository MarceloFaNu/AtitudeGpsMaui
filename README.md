# AtitudeGpsMaui

Este é um projeto de aplicativo de monitoramento de coordenadas GPS para dispositivos móveis feito em .NET MAUI.

A proposta é coletar a coordenadas a cada intervalo de tempo configurável, gerar logs das leituras e depois analisá-las em [outra aplicação](https://github.com/MarceloFaNu/AtitudeGpsSplitter) com a possibilidade de renderizar marcadores nas posições obtidas durante o monitoramento.

O aplicativo possui 3 telas:

1. Tela do monitoramento
2. Tela de configurações
3. Tela do Copiloto

A tela de monitoramento possui as funções de iniciar/parar o monitoramento, limpar os logs e enviar os logs via compartilhamento.

A tela de configurações permite ajustar diversas opções, tais como, nível de sensibilidade da leitura, timeout de espera do resultado da solicitação das coordenadas, casas decimais a serem consideradas nos valores de latitude e longitude, etc.

A tela do Copiloto é onde usuario informa sua real postura de movimento que pode ser:

- Desembarcado Parado
- Desembarcado em Movimento
- Embarcado Parado
- Embarcado em Movimento

O aplicativo está preparado para trabalhar de várias formas no que se refere às regras de leitura que podem ser leitura única, leitura por limite de tempo, média aritimética, dentre outras. Porém, esses modos de operação devem ser selecionados antes da compilação e não foram implementados como opções configuráveis.

Outro detalhe importante é que o aplicativo é executado como um Foreground Service, ou seja, de acordo com as permissões do usuário, mesmo que o aplicativo seja fechado, ele continuará sendo executado em segundo plano, até que seja devidamente encerrado pelo usuário na tela de monitoramento.
