<?php
  $fd = fopen("php://stdin", "r");

  $jsonStr = "";
  while (!feof($fd)) {
    $jsonStr .= fread($fd, 1024);
  }
  fclose($fd);

$json = json_decode($jsonStr, true);
$totalDown = 0;
$totalUp = 0;
foreach($json['arguments']['torrents'] as $torrent)
{
	$totalDown += $torrent['rateDownload']/1024;
	$totalUp += $torrent['rateUpload']/1024;
}
echo "downRate.value ".round($totalDown,2)."\n";
echo "upRate.value ".round($totalUp,2)."\n";
?>
